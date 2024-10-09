using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Entities;
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
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
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

    public User? GetUser(string email, string pass)
    {
        return _userRepository.GetUser(email, pass);
    }

    
    private User? ValidateUser(AuthenticateRequest authenticateRequest)
    {
        if (string.IsNullOrEmpty(authenticateRequest.Email) || string.IsNullOrEmpty(authenticateRequest.Password))
        {
            return null;
        }

        var user = _userRepository.GetUser(authenticateRequest.Email, authenticateRequest.Password);

        if (authenticateRequest.Email == user.Email && authenticateRequest.Password == user.Password)
        {
            return user;
        }

        return null;
    }


    public string? Authenticate(AuthenticateRequest authenticateRequest)
    {
        var user = ValidateUser(authenticateRequest);

        if (user == null)
        {
            return null;
        }

        //Paso 2: Crear el token
        var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Key"])); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

        var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        //Los claims son datos en clave -> valor que nos permite guardar data del usuario.
        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", user.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
        claimsForToken.Add(new Claim("given_name", user.Name));
        //claimsForToken.Add(new Claim(ClaimTypes.Role, authenticateRequest.UserType.ToString())); //quiere usar la API por lo general lo que espera es que se estén usando estas keys.

        var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
        _config["JWT:Issuer"],
        _config["JWT:Audience"],
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        credentials);
        //Pasamos el token a string
        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return tokenToReturn.ToString();
    }



}


