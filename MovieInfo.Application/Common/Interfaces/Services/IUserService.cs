using FluentResults;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IUserService
{
    Task<Result<IEnumerable<GetAllUsersResponse>>> GetAllUsers();

    Task<Result> ChangeUserPassword(ChangeUserPasswordRequest request, string UserName);
}
