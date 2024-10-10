using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Errors;
using MovieInfo.Domain.Interfaces;

namespace MovieInfo.Api.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;
    public AuthController(IAuthService authService, ICurrentUser currentUser) 
    { 
        _authService = authService;
        _currentUser = currentUser;
    }

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Authenticate(AuthenticateRequest authenticateRequest) 
    {
        var result = await _authService.Authenticate(authenticateRequest); 

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(error.Message);

            if (error is AccessForbiddenError) return Forbid(error.Message);

            return BadRequest(result.Errors);
        }

        Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Expires = DateTimeOffset.UtcNow.AddHours(3) });

        return Ok(result.Value.Jwt);
    }

    [HttpGet("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Refresh()
    {
        if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)) return BadRequest("You don't have a refresh token in the cookie!");

        var userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");

        var result = await _authService.RefreshToken(refreshToken, userName);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(error.Message);

            if (error is AccessForbiddenError) return Forbid(error.Message);

            return BadRequest(result.Errors);
        }

        Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Expires = DateTimeOffset.UtcNow.AddHours(3) });

        return Ok(result.Value.Jwt);
    }

}
