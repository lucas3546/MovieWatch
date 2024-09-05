using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Responses;
public record GetFilmByIdResponse(int Id, string Title, TimeSpan Duration, string Type);

