using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public User? GetUser(string email, string pass)
        {
            return _userRepository.GetUser(email, pass);
        }
    }
}
