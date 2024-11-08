using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Application.Services;
using MovieInfo.Domain.Errors;
using MovieInfo.Domain.Interfaces;

namespace MovieInfo.Api.Controllers
{
    
    public class StatiticsController : ApiControllerBase
    {

        private readonly IStatisticsSerivce _statitics;

        public StatiticsController(IStatisticsSerivce statitics)
        {
            _statitics = statitics;
        }


        [HttpGet("statics-one-week-last")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult<IEnumerable<GetLastRegisteredUserResponse>>> LastRegisteredUsers()
        {
            var result = await _statitics.LastRegisteredUsers();

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("statics-porcentage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult<IEnumerable<(string, DateTime)>>> PercentageOfUsersLastMonth()
        {
            var result = await _statitics.PercentageOfUsersLastMonth();

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        


    }
}
