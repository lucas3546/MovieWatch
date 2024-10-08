using MovieInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public bool IsPublic { get; set; }

        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }

        public int? EpisodeId { get; set; }
        public Episode? Episode { get; set; }
    }
}
