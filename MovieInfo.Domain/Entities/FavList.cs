using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class FavList
    {
        public int Id { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Movie>? Movies { get; set; }
        public ICollection<Episode>? Episodes { get; set; }

    }
}
