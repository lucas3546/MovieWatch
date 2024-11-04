using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Responses;
public record GetSeriesFromGenreNameResponse(int Id, string Title, string Synopsis, string Language, string Director, string SerieCoverUrl, IEnumerable<string> Genres);
