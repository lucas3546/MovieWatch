using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }

        public Subscription? Subscription { get; set; }
        public Favorites? Favorites { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
