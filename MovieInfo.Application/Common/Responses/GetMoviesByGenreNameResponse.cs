using MovieInfo.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Responses
{
    public record GetMoviesByGenreNameResponse(int Id, string Title, string Duration, int Year, string Synopsis, string Language, string Director, string? showCaseImageUrl,string movieCoverUrl, MediaModel movieVideo, IEnumerable<string> Genres, int Type = 0);

}
