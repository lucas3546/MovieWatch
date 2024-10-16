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
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly DbSet<Genre> _genre;
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _genre = dbContext.Set<Genre>();
        }

        public async Task<Genre?> GetGenreByName(string Name)
        {
            return await _genre.FirstOrDefaultAsync(g => g.Name == Name);
        }

        public IList<Genre> GetGenresByNames(IList<string> genreNames)
        {
            return _genre.Where(g => genreNames.Contains(g.Name)).ToList();
        }
    }
}
