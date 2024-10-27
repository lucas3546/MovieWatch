using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>?> GetAllMoviesWithMediaAndGenres();
        Task<Movie?> GetMovieByIdWithGenreAndMedia(int id);
        Task<IEnumerable<Movie>?> GetMoviesWithShowcaseImage();
        Task<IEnumerable<Movie>?> GetMoviesByGenreName(string genreName);
        Task<IEnumerable<Movie>?> GetAllMoviesByTitle(string title);
    }
}
