using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Repositories
{
    public interface ISeriesRepository : IGenericRepository<Serie>
    {
        Task<Serie?> GetSerieByIdWithGenreAndCover(int Id);
        Task<IEnumerable<Serie>?> GetAllSerieWithGenres();
        Task<Serie?> GetSerieByIdWithSeason(int Id);

    }
}
