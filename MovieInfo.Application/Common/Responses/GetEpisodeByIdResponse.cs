using MovieInfo.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Responses;
public record GetEpisodeByIdResponse(int id, string title, MediaModel episodeVideo);

