using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly DbSet<User> _users;
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _users = dbContext.Set<User>();
    }
}
