﻿using MovieInfo.Domain.Entities;
﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> GetByNameAsync(string Name)
    {
        return await _users.FirstOrDefaultAsync(x => x.Name == Name);
    }


    public async Task<User?> GetByEmailAsync(string Email)
    {
        return await _users.FirstOrDefaultAsync(x => x.Email == Email);
    }

    public async Task<User?> GetUserWithRoleAndSubscriptionByEmailAsync(string Email)
    {
        return await _users.Include(o => o.Role).Include(o => o.Subscription).FirstOrDefaultAsync(o => o.Email == Email);
    }

    public async Task<User?> GetUserWithRoleAndSubscriptionByNameAsync(string Name)
    {
        return await _users.Include(o => o.Role).Include(o => o.Subscription).FirstOrDefaultAsync(o => o.Name == Name);
    }

    public async Task<IEnumerable<User>> GetUsersWithRoleAndSubscription()
    {
        return await _users.Include(o => o.Role).Include(o => o.Subscription).ToListAsync();
    }
}
