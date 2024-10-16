using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Serie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public string Language { get; set; }
        public List<Genre> Genres { get; set; }
        public ICollection<Season>? Seasons { get; set; }
    }
}
