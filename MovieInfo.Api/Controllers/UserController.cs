using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Application.Services;
using MovieInfo.Domain.Errors;
using MovieInfo.Domain.Interfaces;

namespace MovieInfo.Api.Controllers;

public class UserController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly ICurrentUser _currentUser;
    public UserController(IUserService userService, ICurrentUser currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }
    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = "AdminOrEmployeePolicy")]
    public async Task<ActionResult<IEnumerable<GetAllUsersResponse>>> GetAllUsers()
    {
        var result = await _userService.GetAllUsers();

        if(result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok(result.Value);
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string),StatusCodes.Status403Forbidden)]
    [Authorize]
    public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordRequest request)
    {
        string? userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");

        if (request.CurrentPassword.Equals(request.NewPassword)) return BadRequest(new ApiErrorResponse("ValidationError", "Current Password and New Password they should not be the same"));

        var result = await _userService.ChangeUserPassword(request, userName);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return NoContent();
    }

    [HttpDelete("delete-my-account")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult> DeleteMyAccount()
    {
        string? userName = _currentUser.Name;
        if (userName is null) return Forbid("You don't have permissions to use this");

        var result = await _userService.DeleteUserAsync(userName);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return NoContent();
    }


    [HttpDelete("delete/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUserAsync(string name)
    {
        var user = await _userService.DeleteUserAsync(name);

        if (user.IsFailed)
        {
            var error = user.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", user.Errors));
        }

        return NoContent();
    }

    [HttpPut("update/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateUserAsync(UpdateUserRequest request,string name)
    {
        var user = await _userService.UpdateUserAsync(request, name);

        if (user.IsFailed)
        {
            var error = user.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", user.Errors));
        }

        return NoContent();
    }

}
