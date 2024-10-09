using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories;
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly DbSet<Role> _roles;
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _roles = dbContext.Set<Role>();
    }

    public async Task<Role?> GetRoleByName(string name)
    {
        return await _roles.FirstOrDefaultAsync(o => o.RoleName == name);
    }
}
