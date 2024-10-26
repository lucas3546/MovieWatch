using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Models;
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
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IGenreRepository _genreRepository;
        public SeriesService(ISeriesRepository seriesRepository, IGenreRepository genreRepository)
        {
            _seriesRepository = seriesRepository;
            _genreRepository = genreRepository;
        }

        public async Task<Result<int>> CreateSerieAsync(CreateSerieRequest request)
        {
            var genres = _genreRepository.GetGenresByNames(request.GenreNames);

            if (genres is null) return Result.Fail(new NotFoundError($"Genres not found"));

            var ser = new Serie { Title = request.Title, Synopsis = request.Synopsis, Language = request.Language, Director = request.Director, SerieCoverUrl = request.SerieCoverUrl, Genres = genres };

            await _seriesRepository.AddAsync(ser);

            return Result.Ok(ser.Id);
        }

        public async Task<Result<GetSerieByIdResponse>> GetSerieByIdAsync(int Id)
        {
            var ser = await _seriesRepository.GetSerieByIdWithGenreAndCover(Id);

            if (ser == null) return Result.Fail(new NotFoundError($"Serie with id {Id} not found"));

            var resp = new GetSerieByIdResponse(ser.Id, ser.Title, ser.Synopsis, ser.Language, ser.Director.ToString(), ser.SerieCoverUrl, ser.Genres.Select(g => g.Name));

            return Result.Ok(resp);
        }

        public async Task<Result> UpdateSerieAsync(int Id, UpdateSerieRequest request)
        {
            var ser = await _seriesRepository.GetSerieByIdWithGenreAndCover(Id);

            if (ser == null) return Result.Fail(new NotFoundError($"Serie with id {Id} not found"));

            var gen = _genreRepository.GetGenresByNames(request.GenreNames);

            if (gen.Count == 0) return Result.Fail(new NotFoundError("Genres not found"));

            ser.Title = request.Title;
            ser.Synopsis = request.Synopsis;
            ser.Language = request.Language;
            ser.Director = request.Director;
            ser.Genres = gen;
            ser.SerieCoverUrl = request.SerieCoverUrl;

            await _seriesRepository.UpdateAsync(ser);

            return Result.Ok();
        }

        public async Task<Result> DeleteSerieAsync(int Id)
        {
            var ser = await _seriesRepository.GetSerieByIdWithGenreAndCover(Id);

            if (ser == null) return Result.Fail(new NotFoundError($"Serie with id {Id} not found"));

            await _seriesRepository.DeleteAsync(ser);

            return Result.Ok();
        }

        public async Task<Result<IEnumerable<GetAllSerieResponse>>> GetAllSerieAsync()
        {
            var ser = await _seriesRepository.GetAllSerieWithGenres();

            if (ser == null) return Result.Fail(new NotFoundError("An error ocurred when trying to get all series"));

            var resp = ser.Select(s => new GetAllSerieResponse(s.Id, s.Title, s.SerieCoverUrl, s.Genres.Select(g => g.Name)));

            return Result.Ok(resp);
        }
    }
}
