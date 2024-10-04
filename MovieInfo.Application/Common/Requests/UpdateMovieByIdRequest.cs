using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public class UpdateMovieByIdRequest
    {
        [MaxLength(30)]
        public required string Title { get; set; }
        [Range(0.25, 4)]
        public TimeSpan Duration { get; set; }
        [MaxLength(100)]
        public string Synopsis { get; set; }
        [MaxLength(20)]
        public string Language { get; set; }
    }
}
