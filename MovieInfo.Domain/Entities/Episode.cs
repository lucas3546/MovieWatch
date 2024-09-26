using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }
    }
}
