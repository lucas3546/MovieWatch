using Microsoft.EntityFrameworkCore;
using MovieInfo.Domain.Constants;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence;
public static class ApplicationDbContextSeed
{
    public static void Seed(this ModelBuilder builder)
    {
        SeedRoles(builder);
    }


    private static void SeedRoles(this ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
        new Role { Id = 1, RoleName = Roles.User },
        new Role { Id = 2, RoleName = Roles.Employee },
        new Role { Id = 3, RoleName = Roles.Admin });
    }


}