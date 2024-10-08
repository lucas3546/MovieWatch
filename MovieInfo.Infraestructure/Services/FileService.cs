using Microsoft.AspNetCore.Http;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Services
{
    public class FileService : IFileService
    {
        public async Task<(string fileName, string fileType, bool isPublic)> SaveFileAsync(IFormFile file, bool isPublic)
        {
            string directoryPath = isPublic ? FilePaths.PublicMediaPath : FilePaths.ProtectedMediaPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileExtension = Path.GetExtension(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension; 
            string fullPath = Path.Combine(directoryPath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            string fileType = fileExtension;

            return (uniqueFileName, fileType, isPublic);
        }
    }
}
