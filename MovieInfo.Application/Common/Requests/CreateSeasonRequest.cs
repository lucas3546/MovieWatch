using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public class CreateSeasonRequest
    {
        public int SeasonNumber { get; set; }
        public int SerieId { set; get; }

    }
}
