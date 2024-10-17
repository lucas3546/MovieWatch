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
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IFileService _fileService;
        public MovieService(IMovieRepository movieRepository, IFileService fileService, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _fileService = fileService;
            _genreRepository = genreRepository;
        }

        public async Task<Result<IEnumerable<GetAllMoviesResponse>>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMoviesWithMediaAndGenres();

            if (movies == null) return Result.Fail("An error ocurred when trying to get all movies");

            var response = movies.Select(o =>
            new GetAllMoviesResponse(o.Id, o.Title,
            new MediaModel(o.MovieCover.FileName, o.MovieCover.FileExtension, o.MovieCover.IsPublic),
            o.Genres.Select(o => o.Name)));

            return Result.Ok(response);
        }

        public async Task<Result<int>> CreateMovieAsync(CreateMovieRequest request)
        {
            string movieCoverMediaType = MediaHelper.GetMediaType(request.MovieCover);
            if (!movieCoverMediaType.Equals("image") || movieCoverMediaType.Equals("unknow")) return Result.Fail(new UnsupportedMediaTypeError($"Only images are allowed."));

            string movieVideoMediaType = MediaHelper.GetMediaType(request.MovieVideo);
            if (!movieVideoMediaType.Equals("video") || movieVideoMediaType.Equals("unknow")) return Result.Fail(new UnsupportedMediaTypeError($"Only videos are allowed."));

            Media movieCover;
            Media movieVideo;

            try
            {
                var (coverFileName, coverFileType, isCoverPublic) = await _fileService.SaveFileAsync(request.MovieCover, true);
                movieCover = new Media(coverFileName, coverFileType, isCoverPublic);


                var (videoFileName, videoFileType, isVideoPublic) = await _fileService.SaveFileAsync(request.MovieVideo, false);
                movieVideo = new Media(videoFileName, videoFileType, isVideoPublic);
            }
            catch (Exception ex)
            {
                return Result.Fail(new FileSaveError("File save error", ex.Message));
            }

            var genres = _genreRepository.GetGenresByNames(request.GenreNames);

            if (genres is null) return Result.Fail(new NotFoundError($"Genres not found"));

            var mov = new Movie { Title = request.Title, Duration = TimeSpan.FromHours(request.Duration), Year = request.Year, Director = request.Director, Synopsis = request.Synopsis, Language = request.Language, MovieCover = movieCover, MovieVideo = movieVideo,Genres = genres};

            await _movieRepository.AddAsync(mov);

            return Result.Ok(mov.Id);
        }

        public async Task<Result<GetMovieByIdResponse>> GetMovieByIdAsync(int Id)
        {
            var mov = await _movieRepository.GetMovieByIdWithGenreAndMedia(Id);

            if (mov == null) return Result.Fail(new NotFoundError($"Film with id {Id} not found"));

            MediaModel movieCover = new MediaModel(mov.MovieCover.FileName, mov.MovieCover.FileExtension, mov.MovieCover.IsPublic);
            MediaModel movieVideo = new MediaModel(mov.MovieVideo.FileName, mov.MovieVideo.FileExtension, mov.MovieVideo.IsPublic);

            var response = new GetMovieByIdResponse(mov.Id, mov.Title, mov.Duration, mov.Year,mov.Synopsis, mov.Language, mov.Director, movieCover, movieVideo, mov.Genres.Select(o => o.Name));

            return Result.Ok(response);
        }

        public async Task<Result<IEnumerable<GetMoviesByGenreNameResponse>>> GetMoviesByGenreName(string genreName)
        {
            var movies = await _movieRepository.GetMoviesByGenreName(genreName);

            if (movies == null) return Result.Fail(new NotFoundError($"Error when get movies by genre"));


            var response = movies.Select(o =>
            new GetMoviesByGenreNameResponse(o.Id, o.Title, o.Duration, o.Year, o.Synopsis, o.Language, o.Director,
            new MediaModel(o.MovieCover.FileName, o.MovieCover.FileExtension, o.MovieCover.IsPublic),
            new MediaModel(o.MovieVideo.FileName, o.MovieVideo.FileExtension, o.MovieVideo.IsPublic), 
            o.Genres.Select(o => o.Name)));

            return Result.Ok(response);
        }

        public async Task<Result> UpdateMovieByIdAsync(int id, UpdateMovieByIdRequest request)
        {
            var movie = await _movieRepository.GetMovieByIdWithGenreAndMedia(id);

            if (movie == null) return Result.Fail(new NotFoundError($"Film with id {id} not found"));

            Media movieCover;
            Media movieVideo;

            try
            {
                bool deleteMovieCoverResult = _fileService.DeleteFile(movie.MovieCover.FileName, movie.MovieCover.IsPublic);
                bool deleteMovieVideoResult = _fileService.DeleteFile(movie.MovieVideo.FileName, movie.MovieVideo.IsPublic);
                if (deleteMovieCoverResult is false || deleteMovieVideoResult is false)
                {
                    return Result.Fail("There are an error when deleting Movie Cover or Movie Video, they may not be found in the file system");
                }
            }
            catch(Exception ex)
            {
                return Result.Fail($"File delete error: {ex.Message}");
            }

            try
            {

                var (coverFileName, coverFileType, isCoverPublic) = await _fileService.SaveFileAsync(request.MovieCover, true);
                movieCover = new Media(coverFileName, coverFileType, isCoverPublic);


                var (videoFileName, videoFileType, isVideoPublic) = await _fileService.SaveFileAsync(request.MovieVideo, false);
                movieVideo = new Media(videoFileName, videoFileType, isVideoPublic);
            }
            catch (Exception ex)
            {
                return Result.Fail(new FileSaveError("File save error", ex.Message));
            }

            var genres = _genreRepository.GetGenresByNames(request.GenreNames);
            if (genres.Count == 0) return Result.Fail(new NotFoundError("Genres not found"));

            movie.Title = request.Title;
            movie.Duration = TimeSpan.FromHours(request.Duration);
            movie.Synopsis = request.Synopsis;
            movie.Language = request.Language;
            movie.Director = request.Director;
            movie.Year = request.Year;
            movie.Genres = genres;
            movie.MovieCover = movieCover;
            movie.MovieVideo = movieVideo;

            await _movieRepository.UpdateAsync(movie);

            return Result.Ok();
        }

        public async Task<Result> DeleteMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdWithGenreAndMedia(id);

            if (movie == null) return Result.Fail(new NotFoundError($"Film with id {id} not found"));

            await _movieRepository.DeleteAsync(movie);

            try
            {
                bool deleteMovieCoverResult = _fileService.DeleteFile(movie.MovieCover.FileName, movie.MovieCover.IsPublic);
                bool deleteMovieVideoResult = _fileService.DeleteFile(movie.MovieVideo.FileName, movie.MovieVideo.IsPublic);
                if(deleteMovieCoverResult is false || deleteMovieVideoResult is false)
                {
                    return Result.Fail("There are an error when deleting Movie Cover or Movie Video, they may not be found in the file system");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail($"{ex.Message}");
            }

            return Result.Ok();
        }


    }
}
