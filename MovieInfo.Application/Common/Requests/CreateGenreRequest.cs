using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public class CreateGenreRequest
    {
        [MaxLength(30)]
        public required string Name { get; set; }
    }
}
