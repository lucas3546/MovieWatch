using MovieInfo.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Responses
{
    public record GetMovieByIdResponse(int Id, string Title, TimeSpan Duration, string Synopsis, string Language, string Director, MediaModel media, IEnumerable<string> Genres);
}
