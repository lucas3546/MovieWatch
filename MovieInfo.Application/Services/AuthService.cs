using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services;
public class AuthService : IAuthService
{
    private readonly  IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<int>> RegisterAsync(RegisterUserRequest request)
    {
        var user = new User
        {
            Name = request.UserName,
            Password = request.Password,
            Email = request.Email,
        };

        await _userRepository.AddAsync(user);

        return Result.Ok(user.Id);
    }
}
