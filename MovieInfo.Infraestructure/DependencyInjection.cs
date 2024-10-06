using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Infraestructure.Persistence;
using MovieInfo.Infraestructure.Persistence.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IFilmRepository, FilmRepository>();
        services.AddScoped<IUserRepository, UserRepository>(); 
        services.AddScoped<IMovieRepository, MovieRepository>();
        return services;
    }
}
