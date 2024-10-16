using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Extensions;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers
{
    public class GenreController : ApiControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateGenreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetAllErrors());

            var result = await _genreService.CreateGenreAsync(request);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _genreService.GetAllGenreAsync();

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }

            return Ok(result.Value);
        }

        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateGenreById(int id, UpdateGenreRequest request)
        {
            var mov = await _genreService.UpdateGenreByIdAsync(id, request);

            if (mov.IsFailed)
            {
                var error = mov.Errors.First();

                if (error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteGenreById(int id)
        {
            var mov = await _genreService.DeleteGenreByIdAsync(id);

            if (mov.IsFailed)
            {
                var error = mov.Errors.First();

                if (error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }

            return Ok();
        }
    }
}
