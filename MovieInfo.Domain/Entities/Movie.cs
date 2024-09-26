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
        public string Language { get; set; }
        public TimeSpan Duration { get; set; } //Reminder: TimeSpan.FromHours(2.5) for example
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}
