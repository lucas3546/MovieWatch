using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Repositories;
public interface IFileService
{
    Task<(string fileName, string fileType, bool isPublic)> SaveFileAsync(IFormFile file, bool isPublic);
}
