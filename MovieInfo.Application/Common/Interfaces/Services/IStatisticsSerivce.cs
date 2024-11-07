using FluentResults;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services
{
    public interface IStatisticsSerivce
    {
        Task<Result<IEnumerable<GetLastRegisteredUserResponse>>> LastRegisteredUsers();
        Task<Result<double>> PercentageOfUsersLastMonth();
    }
}
