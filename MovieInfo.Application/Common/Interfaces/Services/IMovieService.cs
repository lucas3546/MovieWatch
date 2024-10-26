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
    public interface IMovieService
    {
        Task<Result<int>> CreateMovieAsync(CreateMovieRequest request);
        Task<Result<GetMovieByIdResponse>> GetMovieByIdAsync(int Id);
        Task<Result<IEnumerable<GetAllMoviesResponse>>> GetAllMovies();
        Task<Result<IEnumerable<GetMoviesWithShowcaseImage>>> GetMoviesWithShowcaseImage();
        Task<Result<IEnumerable<GetMoviesByGenreNameResponse>>> GetMoviesByGenreName(string genreName);
        Task<Result> UpdateMovieByIdAsync(int id, UpdateMovieByIdRequest request);
        Task<Result> DeleteMovieByIdAsync(int id);
    }
}
