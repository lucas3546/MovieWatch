using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Helpers;
public static class MediaHelper
{
    private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
    private static readonly string[] VideoExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm" };

    public static string GetMediaType(IFormFile file)
    {
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (Array.Exists(ImageExtensions, ext => ext == extension))
            {
                return "image";
            }

            if (Array.Exists(VideoExtensions, ext => ext == extension))
            {
                return "video";
            }
        }

        return "unknown";
    }
}