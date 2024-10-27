using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public record UpdateSeasonRequest
    {
        [Range(1,100)]
        public int SeasonNumber { get; set; }
    }
}
