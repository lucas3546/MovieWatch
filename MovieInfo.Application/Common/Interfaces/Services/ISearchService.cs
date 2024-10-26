using FluentResults;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface ISearchService
{
    Task<Result<IEnumerable<SearchMoviesResponseByTitle>>> SearchMoviesAndSeriesByTitle(string Title);
}
