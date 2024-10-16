using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int SerieId { set; get; }
        public Serie Serie { get; set; }
        public ICollection<Episode>? Episodes { get; set; }
    }
}
