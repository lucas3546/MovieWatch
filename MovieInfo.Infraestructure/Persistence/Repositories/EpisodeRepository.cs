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
    public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        private readonly DbSet<Episode> _episodes;
        public EpisodeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _episodes = dbContext.Set<Episode>();
        }


    }
}
