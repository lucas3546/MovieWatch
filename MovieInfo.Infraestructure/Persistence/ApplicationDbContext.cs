using Microsoft.EntityFrameworkCore;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TO DO: Export this to individual files
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Film)
                .WithMany(m => m.Rating)
                .HasForeignKey(r => r.FilmId);
        }
    }
}
