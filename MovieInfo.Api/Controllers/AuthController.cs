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

    [HttpPost] //Vamos a usar un POST ya que debemos enviar los datos para hacer el login
    public ActionResult<string> Authenticate(AuthenticateRequest authenticateRequest) //Enviamos como parámetro la clase que creamos arriba
    {
        string? token = _authService.Authenticate(authenticateRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

        if (token == null || token == String.Empty)
        {
            return BadRequest("Incorrect user or password");
        }
        {
            return Ok(token); 
        }
        
    }




}
