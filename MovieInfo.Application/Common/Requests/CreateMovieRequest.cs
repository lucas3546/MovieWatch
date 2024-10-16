using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public class CreateMovieRequest
    {
        [MaxLength(30)]
        public required string Title { get; set; }
        [Range(0.25, 4)]
        public required double Duration { get; set; }
        [MaxLength(100)]
        public required string Synopsis { get; set; }
        [MaxLength(20)]
        public required string Language { get; set; }
        public required List<string> GenreNames { get; set; }

        public int Year { get; set; }

        public required string Director { get; set; }

        public required IFormFile MovieCover {  get; set; }

        public required IFormFile MovieVideo { get; set; }
    }
}
