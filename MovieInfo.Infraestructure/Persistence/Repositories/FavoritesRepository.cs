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
    public class FavoritesRepository : GenericRepository<Favorites>, IFavoritesRepository
    {
        private readonly DbSet<Favorites> _favorites;
        public FavoritesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _favorites = dbContext.Set<Favorites>();
        }

        public async Task<Favorites?> GetFavoritesFromUserAsync(int userId)
        {
            return await _favorites.Include(o => o.Movies).Include(o => o.Series).FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
