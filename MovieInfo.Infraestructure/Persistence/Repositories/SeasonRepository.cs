using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories;
public class SeasonRepository : GenericRepository<Season>, ISeasonRepository
{
    private readonly DbSet<Season> _seasons;
    public SeasonRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _seasons = dbContext.Set<Season>();
    }
}
