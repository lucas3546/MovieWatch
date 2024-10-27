using FluentResults;
using MovieInfo.Application.Common.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IEpisodeService
{
    Task<Result<int>> AddEpisodeToSeason(CreateEpisodeRequest request);
}
