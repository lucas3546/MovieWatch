using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers;

public class SubscriptionController : ApiControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("add-to-user-from-payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult> CheckPaymentAndCreateSubscriptionForUser([FromBody] long PaymentId)
    {
        var result = await _subscriptionService.AddSubscriptionToUserFromPayment(PaymentId);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok();
    }

    [HttpPost("create-preference")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> CreateSubscriptionPreference(CreateSubscriptionPreferenceRequest request)
    {
        var result = await _subscriptionService.CreateSubscriptionPreference(request);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }

        return Ok(result.Value);
    }


    [HttpGet("get-actual-preference")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetSubscriptionPreferenceResponse>> GetSubscriptionPreference()
    {
        var result = await _subscriptionService.GetSubscriptionPreference();
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }
        return Ok(result.Value);
    }

    [HttpPut("update-actual-preference")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateSubscriptionPreference(UpdateSubscriptionPreferenceRequest request)
    {
        var result = await _subscriptionService.UpdateSubscriptionPreference(request);
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }
        return NoContent();
    }

    [HttpDelete("remove-actual-preference")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RemoveActualSubscriptionPreference()
    {
        var result = await _subscriptionService.RemoveActualSubscription();
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }
        return NoContent();
    }
    [HttpPut("invalidate/{userName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> InvalidateSubscriptionToUser(string userName)
    {
        var result = await _subscriptionService.InvalidateSubscriptionToUser(userName);
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

            return BadRequest(new ApiErrorResponse("Errors", result.Errors));
        }
        return NoContent();
    }
}
