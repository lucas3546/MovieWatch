using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Extensions;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers
{
    public class MoviesController : ApiControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(500000000)]//500 mb
        public async Task<IActionResult> Create([FromForm] CreateMovieRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.GetAllErrors());

            var result = await _movieService.CreateMovieAsync(request);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _movieService.GetMovieByIdAsync(id);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if(error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [NonAction]
        public async Task<IActionResult> UpdateMovieById(int id, UpdateMovieByIdRequest request)
        {
            var mov = await _movieService.GetMovieByIdAsync(id);

            if (mov.IsFailed)
            {
                var error = mov.Errors.First();

                if (error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }

            return Ok(mov.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [NonAction]
        public async Task<IActionResult> DeleteMovieById(int id)
        {
            var mov = await _movieService.GetMovieByIdAsync(id);

            if (mov.IsFailed)
            {
                var error = mov.Errors.First();

                if (error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }
           

            return Ok(mov.Value);
        }
    }
}
