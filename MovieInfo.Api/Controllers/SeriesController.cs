using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Application.Services;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;

namespace MovieInfo.Api.Controllers
{
    public class SeriesController : ApiControllerBase
    {
        private readonly ISeriesService _seriesService;
        private readonly IEpisodeService _episodeService;
        public SeriesController(ISeriesService seriesService, IEpisodeService episodeService)
        {
            _seriesService = seriesService;
            _episodeService = episodeService;
        }

        [HttpGet("get/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetSerieByIdResponse>> GetSerieByIdAsync(int Id)
        {
            var result = await _seriesService.GetSerieByIdAsync(Id);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult<int>> CreateSerieAsync(CreateSerieRequest request)
        {
            var result = await _seriesService.CreateSerieAsync(request);

            if (result.IsFailed)
            {
                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpPut("update/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> UpdateSerieAsync(int Id, UpdateSerieRequest request)
        {
            var ser = await _seriesService.UpdateSerieAsync(Id, request);

            if (ser.IsFailed)
            {
                var error = ser.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", ser.Errors));
            }

            return NoContent();
        }

        [HttpDelete("delete/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> DeleteSerieAsync(int Id)
        {
            var ser = await _seriesService.DeleteSerieAsync(Id);

            if (ser.IsFailed)
            {
                var error = ser.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", ser.Errors));
            }

            return NoContent();
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetAllSerieResponse>>> GetAllSerieAsync()
        {
            var result = await _seriesService.GetAllSerieAsync();

            if (result.IsFailed)
            {
                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("get-series-by-genre/{genreName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GetSeriesFromGenreNameResponse>>> GetSeriesFromGenreName(string genreName)
        {
            var result = await _seriesService.GetSeriesFromGenreName(genreName);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpPost("add-season")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> CreateSeasonAsync(CreateSeasonRequest request)
        {
            var result = await _seriesService.AddSeasonToSeriesAsync(request);

            if (result.IsFailed)
            {
                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("get-seasons-from-serie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetSeasonsFromSerieResponse>>> GetAllSeasonsFromSerie(int id)
        {
            var result = await _seriesService.GetSeasonsFromSerieAsync(id);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }


        [HttpDelete("delete-season-from-serie/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> DeleteSeasonFromSerieAsync(int idSeason)
        {
            var season = await _seriesService.DeleteSeasonToSerieAsync(idSeason);

            if (season.IsFailed)
            {
                var error = season.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", season.Errors));
            }

            return NoContent();
        }

        [HttpPut("update-season/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> UpdateSeasonToSerieAsync(int id, UpdateSeasonRequest request)
        {
            var sea = await _seriesService.UpdateSeasonToSerieAsync(id, request);

            if (sea.IsFailed)
            {
                var error = sea.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", sea.Errors));
            }

            return NoContent();
        }

        [HttpPost("add-episode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> CreateEpisode([FromForm]CreateEpisodeRequest request)
        {
            var result = await _episodeService.AddEpisodeToSeason(request);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        [HttpGet("get-episodes-from-season/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetEpisodeFromSeasonResponse>>> GetEpisodesFromSeasonAsync(int id)
        {
            var result = await _episodeService.GetEpisodeFromSeasonAsync(id);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }



        [HttpGet("get-episode/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetEpisodeByIdResponse>> GetEpisodeById(int id)
        {
            var result = await _episodeService.GetEpisodeById(id);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors));
            }

            return Ok(result.Value);
        }

        //Episode

        [HttpDelete("delete-episode/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> DeleteEpisodeAsync(int Id)
        {
            var episode = await _episodeService.DeleteEpisodeByIdAsync(Id);

            if (episode.IsFailed)
            {
                var error = episode.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", episode.Errors));
            }

            return NoContent();
        }

        [HttpPut("update-episode/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOrEmployeePolicy")]
        public async Task<ActionResult> UpdateEpisodeAsync(int id, UpdateEpisodeByIdRequest request)
        {
            var episode = await _episodeService.UpdateEpisodeByIdAsync(id, request);

            if (episode.IsFailed)
            {
                var error = episode.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("NotFound", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", episode.Errors));
            }

            return NoContent();
        }

    }
}
