using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Interface
{
    public interface IUserRepository
    {
        User? GetUser(string email, string pass);
    }
}
