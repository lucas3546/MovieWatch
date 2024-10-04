using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IFilmService, FilmService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
