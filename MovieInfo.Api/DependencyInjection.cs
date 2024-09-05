namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

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

        return services;
    }
}
