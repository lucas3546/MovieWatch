using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers;

public class SubscriptionController : ApiControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("Add")]
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
}
