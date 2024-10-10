using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using MovieInfo.Api.Services;
using MovieInfo.Domain.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("SubscriptionActivePolicy", policy =>
            {
                policy.RequireClaim("subscriptionState", "Active");
            });

            options.AddPolicy("AdminOrEmployeePolicy", policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") || context.User.IsInRole("Employee")
                );
            });

        });

        services.AddSwaggerGen(setupAction =>
        {
            setupAction.AddSecurityDefinition("MovieWatch", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Enter the Json Web Token:"
            });

            setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "MovieWatch"
                        }
                    }, new List<string>()
                }
                });
        });
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
