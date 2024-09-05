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
    public class FilmRepository : GenericRepository<Film>, IFilmRepository
    {
        private readonly DbSet<Film> _films;
        public FilmRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _films = dbContext.Set<Film>();
        }
    }
}
