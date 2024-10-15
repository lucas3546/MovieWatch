using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public string Language { get; set; }
        public TimeSpan Duration { get; set; } //Reminder: TimeSpan.FromHours(2.5) for example
        public IList<Genre> Genres { get; set; }

        public Media MovieCover { get; set; }
        public Media MovieVideo { get; set; }
    }
}
