using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IFileService
{
    Task<(string fileName, string fileType, bool isPublic)> SaveFileAsync(IFormFile file, bool isPublic);

    bool DeleteFile(string fileName, bool isPublic);

}
