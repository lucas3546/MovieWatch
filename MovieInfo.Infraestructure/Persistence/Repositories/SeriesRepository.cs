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
    public class SeriesRepository : GenericRepository<Serie>, ISeriesRepository
    {
        private readonly DbSet<Serie> _serie;
        public SeriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _serie = dbContext.Set<Serie>();
        }

        public async Task<Serie?> GetSerieByIdWithGenreAndCover(int Id)
        {
            return await _serie.Include(o => o.Genres).Include(o => o.Seasons).FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<IEnumerable<Serie>?> GetAllSerieWithGenres()
        {
            return await _serie.Include(o => o.Genres).ToListAsync();
        }

        public async Task<Serie?> GetSerieByIdWithSeason(int Id)
        {
            return await _serie.Include(o => o.Seasons).FirstOrDefaultAsync(o => o.Id == Id);
        }
        
        public async Task<IEnumerable<Serie>?> GetAllSeriesByTitle(string title)
        {
            return await _serie.Include(o => o.Genres).Where(o => EF.Functions.Like(o.Title, $"%{title}%")).ToListAsync();
        }

        public async Task<IEnumerable<Serie>?> GetSeriesByGenreName(string genreName)
        {
            return await _serie.Include(o => o.Genres).Where(m => m.Genres.Any(g => g.Name == genreName)).ToListAsync();

        }



    }
}
