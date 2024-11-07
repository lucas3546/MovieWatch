using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services
{
    public class StatisticsService : IStatisticsSerivce
    {
        private readonly IUserRepository _userRepository;
        public StatisticsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<GetLastRegisteredUserResponse>>> LastRegisteredUsers()
        {
            //Metodo usuarios registrados en los ultimos 7 dias del total.

            var users = await _userRepository.GetAllAsync();

            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

            var recentUsers = users.Where(u => u.RegistrationUser >= oneWeekAgo).Select(u => (u.Name, u.RegistrationUser));

            var response = recentUsers.Select(u => new GetLastRegisteredUserResponse(u.Name, u.RegistrationUser));


            return Result.Ok(response);
        }

        public async Task<Result<double>> PercentageOfUsersLastMonth()
        {

            //Metodo para validar el porcentaje de los usuarios registrados en los ultimos 30 dias del total.
            var users = await _userRepository.GetAllAsync();
            if (users == null) return Result.Fail("Error al obtener el ususario");
            
            var totalUser = users.Count();
            if (totalUser <= 0) return Result.Fail("No se encuentran usuarios registrados");

            var oneWeekAgo = DateTime.UtcNow.AddDays(-30);
            var recentUsers = users.Where(u => u.RegistrationUser >= oneWeekAgo);
            var totalUserRecent = recentUsers.Count();

            
            var porcentaje = (totalUserRecent / (double)totalUser) * 100;

            return Result.Ok(porcentaje);
        }




    }
}
