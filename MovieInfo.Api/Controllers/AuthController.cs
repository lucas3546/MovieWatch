using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Errors;
using MovieInfo.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

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
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok(result.Value);
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Authenticate(AuthenticateRequest authenticateRequest) 
    {
        var result = await _authService.Authenticate(authenticateRequest); 

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            if (error is AccessForbiddenError) return BadRequest(new ApiErrorResponse("AuthenticationFailed", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken, new CookieOptions() { HttpOnly = true, Secure=false, SameSite = SameSiteMode.Unspecified, Expires = DateTimeOffset.UtcNow.AddHours(3), Domain = "localhost" });

        return Ok(result.Value.Jwt);
    }

    [HttpGet("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize]
    public async Task<ActionResult<string>> Refresh()
    {
        //if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)) return BadRequest("You don't have a refresh token in the cookie!");

        var userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");
        
        var result = await _authService.RefreshToken("refreshToken", userName);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            if (error is AccessForbiddenError) return Forbid(error.Message);

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        Response.Cookies.Append("X-Refresh-Token", result.Value.RefreshToken, new CookieOptions() { HttpOnly = true, Secure = false, SameSite = SameSiteMode.Unspecified, Expires = DateTimeOffset.UtcNow.AddHours(3), Domain = "localhost" });

        return Ok(result.Value.Jwt);
    }

    [HttpPost("request-reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RequestResetPassword([FromBody][EmailAddress] string Email)
    {
        var result = await _authService.RequestResetPassword(Email);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok();
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var result = await _authService.ResetPassword(request);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok();
    }

}
