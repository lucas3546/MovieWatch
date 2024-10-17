using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly DbSet<Movie> _movies;
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _movies = dbContext.Set<Movie>();
        }

        public async Task<Movie?> GetMovieByIdWithGenreAndMedia(int id)
        {
            return await _movies.Include(o => o.Genres).Include(o => o.MovieVideo).Include(o => o.MovieCover).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Movie>?> GetMoviesByGenreName(string genreName)
        {
            return await _movies.Include(o => o.Genres).Include(o => o.MovieCover).Include(o => o.MovieVideo).Where(m => m.Genres.Any(g => g.Name == genreName)).ToListAsync();

        }

        public async Task<IEnumerable<Movie>?> GetAllMoviesWithMediaAndGenres()
        {
            return await _movies.Include(o => o.Genres).Include(o => o.MovieCover).ToListAsync();
        }
    }
}
