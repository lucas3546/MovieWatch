using Microsoft.Extensions.FileProviders;
using MovieInfo.Domain.Configurations;
using MovieInfo.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

Directory.CreateDirectory(FilePaths.PublicMediaPath);
Directory.CreateDirectory(FilePaths.ProtectedMediaPath);

builder.Services.AddApplicationServices();
builder.Services.AddInfraestructureServices(builder.Configuration);
builder.Services.AddApiServices();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
