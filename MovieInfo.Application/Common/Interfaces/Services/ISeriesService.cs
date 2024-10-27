using FluentResults;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services
{
    public interface ISeriesService
    {
        Task<Result<int>> CreateSerieAsync(CreateSerieRequest request);
        Task<Result<GetSerieByIdResponse>> GetSerieByIdAsync(int Id);
        Task<Result> UpdateSerieAsync(int Id, UpdateSerieRequest request);
        Task<Result> DeleteSerieAsync(int Id);
        Task<Result<IEnumerable<GetAllSerieResponse>>> GetAllSerieAsync();
        Task<Result<int>> AddSeasonToSeriesAsync(CreateSeasonRequest request);
        Task<Result<IEnumerable<GetSeasonsFromSerieResponse>>> GetSeasonsFromSerieAsync(int id);
        Task<Result> DeleteSeasonToSerieAsync(int idSerie, int seasonNumber);
        Task<Result> UpdateSeasonToSerieAsync(int id, UpdateSeasonRequest request);
    }
}
