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

namespace MovieInfo.Application.Services;
public class FavoritesService : IFavoritesService
{
    private readonly IFavoritesRepository _favoriteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ISeriesRepository _seriesRepository;
    public FavoritesService(IFavoritesRepository favoriteRepository, IUserRepository userRepository, IMovieRepository movieRepository, ISeriesRepository seriesRepository)
    {
        _favoriteRepository = favoriteRepository;
        _userRepository = userRepository;
        _movieRepository = movieRepository;
        _seriesRepository = seriesRepository;
    }
    public async Task<Result> AddToFavorites(AddToFavoritesRequest request, string UserName)
    {
        var user = await _userRepository.GetByNameAsync(UserName);

        if (user == null) return Result.Fail(new NotFoundError("User not found"));

        var favorites = await _favoriteRepository.GetFavoritesFromUserAsync(user.Id);

        if (favorites == null) return Result.Fail(new NotFoundError($"User doesn't have initialized a favorites."));

        switch (request.type)
        {
            case 0: 
                var movie = await _movieRepository.GetByIdAsync(request.Id);

                if (movie is null) return Result.Fail(new NotFoundError("Movie not found"));

                favorites.Movies.Add(movie);

                await _favoriteRepository.UpdateAsync(favorites);

                break;
            case 1:
                var serie = await _seriesRepository.GetByIdAsync(request.Id);
                if (serie is null) return Result.Fail(new NotFoundError("Serie not found"));

                favorites.Series.Add(serie);

                await _favoriteRepository.UpdateAsync(favorites);

                break;
            default: 
                return Result.Fail("Type should be 0 for movie or 1 for serie.");
        }

        return Result.Ok();

    }

    public async Task<Result<IEnumerable<GetFavoritesFromUserResponse>>> GetFavoritesFromUser(string UserName)
    {
        var user = await _userRepository.GetByNameAsync(UserName);

        if (user == null) return Result.Fail(new NotFoundError("User not found"));

        var favorites = await _favoriteRepository.GetFavoritesFromUserAsync(user.Id);

        if (favorites == null) return Result.Fail(new NotFoundError($"User doesn't have initialized a favorites."));

        var moviesResp = favorites.Movies.Select(o => new GetFavoritesFromUserResponse(o.Id, o.Title, o.MovieCoverUrl, 0));

        var seriesResp = favorites.Series.Select(o => new GetFavoritesFromUserResponse(o.Id, o.Title, o.SerieCoverUrl, 1));

        var response = moviesResp.Concat(seriesResp);

        return Result.Ok(response);
    }

    public async Task<Result> RemoveFromFavorites(int id, string userName, int type)
    {
        var user = await _userRepository.GetByNameAsync(userName);

        if (user == null) return Result.Fail(new NotFoundError("User not found"));

        var favorites = await _favoriteRepository.GetFavoritesFromUserAsync(user.Id);

        if (favorites == null) return Result.Fail(new NotFoundError($"User doesn't have initialized a favorites."));

        switch (type)
        {
            case 0:
                var movie = favorites.Movies.FirstOrDefault(o => o.Id == id);

                if (movie is null) return Result.Fail(new NotFoundError("Movie not found"));

                favorites.Movies.Remove(movie);

                await _favoriteRepository.UpdateAsync(favorites);

                break;
            case 1:
                var serie = favorites.Series.FirstOrDefault(o => o.Id == id);
                if (serie is null) return Result.Fail(new NotFoundError("Serie not found"));

                favorites.Series.Remove(serie);

                await _favoriteRepository.UpdateAsync(favorites);

                break;
            default:
                return Result.Fail("Type should be 0 for movie or 1 for serie.");
        }

        return Result.Ok();
    }
}
