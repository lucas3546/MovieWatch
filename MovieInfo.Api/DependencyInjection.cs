using Microsoft.AspNetCore.Http.Features;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:5173") //TO DO: Configure this port
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials(); 
            });
        });

        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
            x.MultipartBoundaryLengthLimit = int.MaxValue;
            x.MultipartHeadersCountLimit = int.MaxValue;
            x.MultipartHeadersLengthLimit = int.MaxValue;
        });


        return services;
    }
}
