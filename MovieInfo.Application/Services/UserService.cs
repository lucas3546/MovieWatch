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
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
}
