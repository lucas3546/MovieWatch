using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers
{
    public class SearchController : ApiControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet("by-title")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SearchMoviesResponseByTitle>>> SearchContentByTitle([FromQuery] string Title)
        {
            var result = await _searchService.SearchMoviesAndSeriesByTitle(Title);

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
