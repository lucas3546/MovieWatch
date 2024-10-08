using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;

namespace MovieInfo.Api.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) 
    { 
        _authService = authService;
    }

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
}
