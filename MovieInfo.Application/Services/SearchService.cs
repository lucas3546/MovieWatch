using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services;
public class SearchService : ISearchService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISeriesRepository _seriesRepository;
    public SearchService(IMovieRepository movieRepository, ISeriesRepository seriesRepository)
    {
        _movieRepository = movieRepository;
        _seriesRepository = seriesRepository;
    }

    public async Task<Result<IEnumerable<SearchMoviesResponseByTitle>>> SearchMoviesAndSeriesByTitle(string Title)
    {
        var movies = await _movieRepository.GetAllMoviesByTitle(Title);
        if (movies == null) return Result.Fail(new NotFoundError("There are some error when trying to get the movies"));

        var series = await _seriesRepository.GetAllSeriesByTitle(Title);
        if (series == null) return Result.Fail(new NotFoundError("There are some error when trying to get the series"));

        var moviesDto = movies.Select(o => new SearchMoviesResponseByTitle(o.Id, o.Title, o.MovieCoverUrl, o.Genres.Select(o => o.Name), 0));

        var seriesDto = series.Select(o => new SearchMoviesResponseByTitle(o.Id, o.Title, o.SerieCoverUrl, o.Genres.Select(o => o.Name), 1));

        var response = moviesDto.Concat(seriesDto);

        return Result.Ok(response);

    }
}
