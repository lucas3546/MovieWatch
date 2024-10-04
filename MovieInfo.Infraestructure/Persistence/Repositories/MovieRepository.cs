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
    }
}
