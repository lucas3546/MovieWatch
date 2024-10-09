using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Constants;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services;
public class AuthService : IAuthService
{
    private readonly  IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _config = config;
        _roleRepository = roleRepository;
    }
    public async Task<Result<int>> RegisterAsync(RegisterUserRequest request)
    {
        var role = await _roleRepository.GetRoleByName(Roles.User);
        if (role == null) return Result.Fail("An error ocurred with the role.");

        var user = new User
        {
            Name = request.UserName,
            Password = request.Password,
            Email = request.Email,
            Role = role
        };

        await _userRepository.AddAsync(user);

        return Result.Ok(user.Id);
    }
    
    public async Task<Result<AuthenticateResponse>> Authenticate(AuthenticateRequest authenticateRequest)
    {
        var user = await _userRepository.GetUserWithRoleByEmailAsync(authenticateRequest.Email);

        if (user == null) return Result.Fail(new NotFoundError("User not found"));

        if (!user.Password.Equals(authenticateRequest.Password)) return Result.Fail(new AccessForbiddenError("Email or Password are incorrect"));

        //Generate jwt.
        var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Key"])); 

        var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())); 
        claimsForToken.Add(new Claim(ClaimTypes.Name, user.Name));
        claimsForToken.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));

        var jwtSecurityToken = new JwtSecurityToken( 
        _config["JWT:Issuer"],
        _config["JWT:Audience"],
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        credentials);
        var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


        return Result.Ok(new AuthenticateResponse(jwt));
    }

}


