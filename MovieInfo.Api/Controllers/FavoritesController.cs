using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Errors;
using MovieInfo.Domain.Interfaces;

namespace MovieInfo.Api.Controllers;

public class FavoritesController : ApiControllerBase
{
    private readonly IFavoritesService _favoritesService;
    private readonly ICurrentUser _currentUser;
    public FavoritesController(IFavoritesService favoritesService, ICurrentUser currentUser)
    {
        _favoritesService = favoritesService;
        _currentUser = currentUser;
    }
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult> AddToFavorites(AddToFavoritesRequest request)
    {
        string? userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");

        var result = await _favoritesService.AddToFavorites(request, userName);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok();
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetFavoritesFromUserResponse>>> GetFavorites()
    {
        string? userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");

        var result = await _favoritesService.GetFavoritesFromUser(userName);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok(result.Value);
    }

    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult> RemoveFromFavorites(int id, [FromBody] int type)
    {
        string? userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");

        var result = await _favoritesService.RemoveFromFavorites(id, userName, type);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return NoContent();
    }
    
}
