using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers;

public class SubscriptionController : ApiControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("add-from-payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckPaymentAndCreateSubscriptionForUser([FromBody] long PaymentId)
    {
        var result = await _subscriptionService.AddSubscriptionToUserFromPayment(PaymentId);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            if (error is NotFoundError) return NotFound(error.Message);

            return BadRequest(error.Message);
        }

        return Ok();
    }

    [HttpPost("create-preference")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSubscriptionPreference(CreateSubscriptionPreferenceRequest request)
    {
        var result = await _subscriptionService.CreateSubscriptionPreference(request);

        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(error.Message);
        }

        return Ok(result.Value);
    }


    [HttpGet("get-actual-preference")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSubscriptionPreference()
    {
        var result = await _subscriptionService.GetSubscriptionPreference();
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(error.Message);
        }
        return Ok(result.Value);
    }

    [HttpPut("update-actual-preference")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSubscriptionPreference(UpdateSubscriptionPreferenceRequest request)
    {
        var result = await _subscriptionService.UpdateSubscriptionPreference(request);
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(error.Message);
        }
        return NoContent();
    }

    [HttpDelete("remove-actual-preference")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveActualSubscriptionPreference()
    {
        var result = await _subscriptionService.RemoveActualSubscription();
        if (result.IsFailed)
        {
            var error = result.Errors.First();

            return BadRequest(error.Message);
        }
        return NoContent();
    }
}
