using FluentResults;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IFavoritesService
{
    Task<Result> AddToFavorites(AddToFavoritesRequest request, string UserName);
    Task<Result<IEnumerable<GetFavoritesFromUserResponse>>> GetFavoritesFromUser(string UserName);
    Task<Result> RemoveFromFavorites(int id, string userName, int type);
}
