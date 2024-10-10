using Microsoft.EntityFrameworkCore;
using MovieInfo.Domain.Constants;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Enums;
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

        builder.Entity<User>().HasData(
            new User { Id = 1, Name = "Administrador",Email = "administrador@gmail.com", Password = "Administrador1", RoleId = 3 },
            new User { Id = 2, Name = "Empleado", Email = "empleado@gmail.com", Password = "Empleado1", RoleId = 2 },
            new User { Id = 3, Name = "Usuario", Email = "usuario@gmail.com", Password = "Usuario1", RoleId = 1 }

        );

        builder.Entity<Subscription>().HasData(
        new Subscription { Id = 1, StartDate = DateTime.UtcNow, ExpirationDate = DateTime.UtcNow.AddYears(5), State = SubscriptionState.Active, UserId = 1 },
        new Subscription { Id = 2, StartDate = DateTime.UtcNow, ExpirationDate = DateTime.UtcNow.AddYears(5), State = SubscriptionState.Active, UserId = 2 },
        new Subscription { Id = 3, StartDate = DateTime.UtcNow, ExpirationDate = DateTime.UtcNow.AddYears(5), State = SubscriptionState.Active, UserId = 3 }
    );
    }


}