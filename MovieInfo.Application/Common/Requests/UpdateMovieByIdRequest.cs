using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public record UpdateMovieByIdRequest
    {
        [MinLength(5), MaxLength(30)]
        public required string Title { get; set; }
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$", ErrorMessage = "Duration must be in the format hh:mm:ss")]
        public required string Duration { get; set; }
        [MinLength(5), MaxLength(9000)]
        public required string Synopsis { get; set; }
        [MinLength(4), MaxLength(50)]
        public required string Language { get; set; }
        public required List<string> GenreNames { get; set; }
        [Range(1900, 2024)]
        public int Year { get; set; }
        [MinLength(4), MaxLength(50)]
        public required string Director { get; set; }
        [Url]
        public string? ShowCaseImageUrl { get; set; }
        [Url]
        public required string MovieCoverUrl { get; set; }

        public required IFormFile MovieVideo { get; set; }
    }
}
