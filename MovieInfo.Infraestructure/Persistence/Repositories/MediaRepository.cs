using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories;
public class MediaRepository : GenericRepository<Media>, IMediaRepository
{
    private readonly DbSet<Media> _media;
    public MediaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _media = dbContext.Set<Media>();
    }
}