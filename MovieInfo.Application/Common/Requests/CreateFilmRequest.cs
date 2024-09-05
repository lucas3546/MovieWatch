using MovieInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests;
public record CreateFilmRequest
{
    [MaxLength(30)]
    public required string Title { get; set; }
    [Range(0.25, 4)] //25 minutes to 4 hours
    public double Duration { get; set; }
    [Range(0, 1)]
    public FilmType Type { get; set; }
}
