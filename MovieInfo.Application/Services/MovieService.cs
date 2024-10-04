using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result<int>> CreateMovieAsync(CreateMovieRequest request)
        {
            var mov = new Movie { Title = request.Title, Duration = TimeSpan.FromHours(request.Duration), Synopsis = request.Synopsis, Language = request.Language };

            await _movieRepository.AddAsync(mov);

            return Result.Ok(mov.Id);
        }

        public async Task<Result<GetMovieByIdResponse>> GetMovieByIdAsync(int Id)
        {
            var mov = await _movieRepository.GetByIdAsync(Id);

            if (mov == null) return Result.Fail(new NotFoundError($"Film with id {Id} not found"));

            var resp = new GetMovieByIdResponse(mov.Id, mov.Title, mov.Duration, mov.Synopsis, mov.Language);

            return Result.Ok(resp);
        }

        public async Task<Result> UpdateMovieByIdAsync(int id, UpdateMovieByIdRequest request)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null) return Result.Fail(new NotFoundError($"Film with id {id} not found"));

            movie.Title = request.Title;
            movie.Duration = request.Duration;
            movie.Synopsis = request.Synopsis;
            movie.Language = request.Language;

            await _movieRepository.UpdateAsync(movie);

            return Result.Ok();
        }

        public async Task<Result> DeleteMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null) return Result.Fail(new NotFoundError($"Film with id {id} not found"));

            await _movieRepository.DeleteAsync(movie);
            
            return Result.Ok();
        }
    }
}
