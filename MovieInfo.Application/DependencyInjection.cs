using Microsoft.Extensions.DependencyInjection;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Services;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;
public static class DepdencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ISeriesService, SeriesService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IEpisodeService, EpisodeService>();
        services.AddScoped<IFavoritesService, FavoritesService>();
        return services;
    }
}
