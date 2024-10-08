using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Extensions;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers
{/*
    public class FilmsController : ApiControllerBase
    {
        private readonly IFilmService _filmService;
        public FilmsController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateFilmRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.GetAllErrors());

            var result = await _filmService.CreateFilmAsync(request);

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
            var result = await _filmService.GetFilmByIdAsync(id);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if(error is NotFoundError) return NotFound(error.Message);

                return BadRequest(error.Message);
            }

            return Ok(result.Value);
        }
    }*/
}
