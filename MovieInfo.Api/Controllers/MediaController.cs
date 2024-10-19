using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using MovieInfo.Domain.Constants;
using System.Net;

namespace MovieInfo.Api.Controllers;

public class MediaController : ApiControllerBase
{
    private readonly string _publicFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.PublicMediaPath);
    private readonly string _protectedFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.ProtectedMediaPath);


    [HttpGet("public/{fileName}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
    public IActionResult GetPublicFile(string fileName)
    {
        return GetFile(fileName, _publicFileDirectory);
    }

    [HttpGet("protected/{fileName}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileStreamResult))]
    [Authorize(Policy = "SubscriptionActivePolicy")]
    public IActionResult GetProtectedFile(string fileName)
    {
        return GetFile(fileName, _protectedFileDirectory);
    }

    private IActionResult GetFile(string fileName, string fileDirectory)
    {
        var filePath = Path.Combine(fileDirectory, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(new { message = "File not found." });
        }

        var contentType = GetContentType(fileName);
        var rangeHeader = Request.Headers["Range"].ToString();

        if (string.IsNullOrEmpty(rangeHeader))
        {
            return PhysicalFile(filePath, contentType);
        }

        var fileInfo = new FileInfo(filePath);
        var totalSize = fileInfo.Length;
        var range = rangeHeader.Replace("bytes=", "").Split('-');
        var start = long.Parse(range[0]);
        var end = range.Length > 1 && !string.IsNullOrEmpty(range[1]) ? long.Parse(range[1]) : totalSize - 1;
        var length = end - start + 1;

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        fileStream.Seek(start, SeekOrigin.Begin);

        Response.StatusCode = 206; 
        Response.Headers.Add("Accept-Ranges", "bytes");
        Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{totalSize}");

        return File(fileStream, contentType, enableRangeProcessing: true);
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