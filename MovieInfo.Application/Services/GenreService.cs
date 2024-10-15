using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Result<int>> CreateGenreAsync(CreateGenreRequest request)
        {
            var gen = new Genre { Name = request.Name };

            await _genreRepository.AddAsync(gen);

            return Result.Ok(gen.Id);
        }

        public async Task<Result<IEnumerable<GetAllGenreResponse>>> GetAllGenreAsync()
        {
            var gen = await _genreRepository.GetAllAsync();

            var resp = gen.Select(o => new GetAllGenreResponse(o.Id, o.Name));

            return Result.Ok(resp);
        }

        public async Task<Result> UpdateGenreByIdAsync(int id, UpdateGenreRequest request)
        {
            var gen = await _genreRepository.GetByIdAsync(id);

            if (gen == null) return Result.Fail(new NotFoundError($"Genre with id {id} not found"));

            gen.Name = request.Name;

            await _genreRepository.UpdateAsync(gen);

            return Result.Ok();
        }

        public async Task<Result> DeleteGenreByIdAsync(int id)
        {
            var gen = await _genreRepository.GetByIdAsync(id);

            if (gen == null) return Result.Fail(new NotFoundError($"Genre with id {id} not found"));

            await _genreRepository.DeleteAsync(gen);

            return Result.Ok();
        }

        public async Task<Result<GetGenreByNameResponse>> GetGenreByNameAsync(string Name)
        {
            var gen = await _genreRepository.GetGenreByName(Name);

            if (gen == null) return Result.Fail(new NotFoundError($"Genre with name {Name} not found"));

            var resp = new GetGenreByNameResponse(gen.Id, gen.Name);

            return Result.Ok(resp);
        }
    }
}
