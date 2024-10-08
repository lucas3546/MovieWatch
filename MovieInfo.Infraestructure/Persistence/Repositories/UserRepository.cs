using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public User? GetUser(string email, string pass)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == pass);
        }
    }
}
