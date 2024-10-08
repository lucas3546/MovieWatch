using FluentResults;
using MovieInfo.Application.Common.Helpers;
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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IFileService _fileService;
        public MovieService(IMovieRepository movieRepository, IFileService fileService)
        {
            _movieRepository = movieRepository;
            _fileService = fileService;
        }

        public async Task<Result<int>> CreateMovieAsync(CreateMovieRequest request)
        {
            string mediaType = MediaHelper.GetMediaType(request.Image);
            if (!mediaType.Equals("image") || mediaType.Equals("unknow")) return Result.Fail(new UnsupportedMediaTypeError($"Only images are allowed."));
            var media = new Media();
            try
            {
                var (fileName, fileType, isPublic) = await _fileService.SaveFileAsync(request.Image, true);
                media.FileName = fileName;
                media.FileExtension = fileType;
                media.IsPublic = isPublic;

            }
            catch (Exception ex)
            {
                return Result.Fail(new FileSaveError(request.Image.FileName, ex.Message));
            }
            //Arreglar esta mierda
            var genres = new List<Genre>()
            {
                new Genre
                {
                    Name = "test"
                }
            };
            
            var mov = new Movie { Title = request.Title, Duration = TimeSpan.FromHours(request.Duration), Director = request.Director, Synopsis = request.Synopsis, Language = request.Language, Media = media, Genres = genres};

            await _movieRepository.AddAsync(mov);

            return Result.Ok(mov.Id);
        }

        public async Task<Result<GetMovieByIdResponse>> GetMovieByIdAsync(int Id)
        {
            var mov = await _movieRepository.GetMovieByIdWithGenreAndMedia(Id);

            if (mov == null) return Result.Fail(new NotFoundError($"Film with id {Id} not found"));

            MediaModel mediaModel = new MediaModel(mov.Media.FileName, mov.Media.FileExtension, mov.Media.IsPublic);
            var response = new GetMovieByIdResponse(mov.Id, mov.Title, mov.Duration, mov.Synopsis, mov.Language, mov.Director, mediaModel, mov.Genres.Select(o => o.Name));

            return Result.Ok(response);
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
