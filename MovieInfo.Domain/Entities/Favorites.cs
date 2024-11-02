using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Favorites
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public  User User { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<Serie> Series { get; set; } = new List<Serie>();
    }
}
