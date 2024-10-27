using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }
    public async Task<Result<IEnumerable<GetAllUsersResponse>>> GetAllUsers()
    {
        var users = await _userRepository.GetUsersWithRoleAndSubscription();
        if (users == null) return Result.Fail("An error ocurred when trying to get all users");

        var response = users.Select(o => new GetAllUsersResponse(o.Name, o.Email, o.Role.RoleName, o.Subscription?.State.ToString()));

        return Result.Ok(response);
    }

    public async Task<Result> ChangeUserPassword(ChangeUserPasswordRequest request, string UserName)
    {
        var user = await _userRepository.GetByNameAsync(UserName);
        if (user == null) return Result.Fail("An error ocurred when trying to get the user");

        if (!user.Password.Equals(request.CurrentPassword)) return Result.Fail(new AccessForbiddenError("The current password you entered is incorrect."));

        user.Password = request.NewPassword;

        await _userRepository.UpdateAsync(user);

        
        return Result.Ok();
    }

    public async Task<Result> DeleteUserAsync(string name)
    {
        var user = await _userRepository.GetByNameAsync(name);
        if (user == null) return Result.Fail("An error ocurred when trying to get the user");
        await _userRepository.DeleteAsync(user);
        
        return Result.Ok();
    }

    public async Task<Result> UpdateUserAsync(UpdateUserRequest request,string name)
    {
        var user = await _userRepository.GetByNameAsync(name);

        if (user == null)
        {
            return Result.Fail("An error ocurred when trying to get the user");
        }

        var role = await _roleRepository.GetRoleByName(request.Role);

        if (role == null)
        {
            return Result.Fail(new NotFoundError("Role not found"));
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = request.Password;
        user.Role = role;

        await _userRepository.UpdateAsync(user);
        return Result.Ok();
    }




}
