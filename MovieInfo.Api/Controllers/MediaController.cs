using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Domain.Constants;

namespace MovieInfo.Api.Controllers;

public class MediaController : ApiControllerBase
{
    private readonly string _publicFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.PublicMediaPath);
    private readonly string _protectedFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.ProtectedMediaPath);


    [HttpGet("public/{fileName}")]
    public IActionResult GetPublicFile(string fileName)
    {
        var filePath = Path.Combine(_publicFileDirectory, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(new { message = "File not found." });
        }

        var contentType = GetContentType(fileName);

        return PhysicalFile(filePath, contentType);
    }


    [HttpGet("protected/{fileName}")]
    //Agregar el authorize luego al tener resuelto el tema del login y la subscripcion.
    public IActionResult GetProtectedFile(string fileName)
    {
        var filePath = Path.Combine(_protectedFileDirectory, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(new { message = "File not found." });
        }

        var contentType = GetContentType(fileName);

        return PhysicalFile(filePath, contentType);
    }

    private string GetContentType(string fileName)
    {
        var types = new Dictionary<string, string>
        {
            { ".pdf", "application/pdf" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".mp4", "video/mp4" },
            { ".avi", "video/x-msvideo" },
            { ".mov", "video/quicktime" }
        };

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return types.ContainsKey(ext) ? types[ext] : "application/octet-stream"; // Por default
    }
}