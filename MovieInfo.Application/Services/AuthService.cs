using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Constants;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Enums;
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
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IJwtService _jwtService;
    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, ISubscriptionRepository subscriptionRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _subscriptionRepository = subscriptionRepository;
        _jwtService = jwtService;
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
        var user = await _userRepository.GetUserWithRoleAndSubscriptionByEmailAsync(authenticateRequest.Email);

        if (user == null) return Result.Fail(new NotFoundError("User not found"));

        if (!user.Password.Equals(authenticateRequest.Password)) return Result.Fail(new AccessForbiddenError("Email or Password are incorrect"));

        var subscriptionState = user.Subscription == null ? "Inactive" : user.Subscription.State.ToString();

        user.RefreshToken = Guid.NewGuid().ToString();

        await _userRepository.UpdateAsync(user);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())); 
        claimsForToken.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
        claimsForToken.Add(new Claim("role", user.Role.RoleName));
        claimsForToken.Add(new Claim("subscriptionState", subscriptionState));

        string jwt =_jwtService.GenerateToken(claimsForToken);
        
        return Result.Ok(new AuthenticateResponse(jwt, user.RefreshToken));
    }

    public async Task<Result<RefreshTokenResponse>> RefreshToken(string refreshToken, string userName)
    {
        var user = await _userRepository.GetUserWithRoleAndSubscriptionByNameAsync(userName);
        if (user == null) return Result.Fail(new NotFoundError("User not found"));

        if (!refreshToken.Equals(user.RefreshToken)) return Result.Fail(new AccessForbiddenError("Your refresh token is outdated, please log in again!"));

        var subscriptionState = user.Subscription == null ? "Inactive" : user.Subscription.State.ToString();

        user.RefreshToken = Guid.NewGuid().ToString();

        await _userRepository.UpdateAsync(user);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claimsForToken.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
        claimsForToken.Add(new Claim("role", user.Role.RoleName));
        claimsForToken.Add(new Claim("subscriptionState", subscriptionState));

        string jwt = _jwtService.GenerateToken(claimsForToken);

        return Result.Ok(new RefreshTokenResponse(jwt, user.RefreshToken));
    }

}


