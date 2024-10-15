using FluentResults;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services
{
    public interface IGenreService
    {
        Task<Result<int>> CreateGenreAsync(CreateGenreRequest request);
        Task<Result<IEnumerable<GetAllGenreResponse>>> GetAllGenreAsync();
        Task<Result> UpdateGenreByIdAsync(int id, UpdateGenreRequest request);
        Task<Result> DeleteGenreByIdAsync(int id);
        Task<Result<GetGenreByNameResponse>> GetGenreByNameAsync(string Name);
    }
}
