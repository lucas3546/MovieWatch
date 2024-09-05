using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services;
public class FilmService : IFilmService
{
    private readonly IFilmRepository _filmRepository;
    public FilmService(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }

    public async Task<Result<int>> CreateFilmAsync(CreateFilmRequest request)
    {
        var film = new Film { Title = request.Title, Duration = TimeSpan.FromHours(request.Duration), Type = request.Type };

        await _filmRepository.AddAsync(film);

        return Result.Ok(film.Id);
    }

    public async Task<Result<GetFilmByIdResponse>> GetFilmByIdAsync(int Id)
    {
        var film = await _filmRepository.GetByIdAsync(Id);

        if (film is null) return Result.Fail(new NotFoundError($"Film with id {Id} not found"));

        var response = new GetFilmByIdResponse(film.Id, film.Title, film.Duration, film.Type.ToString());

        return Result.Ok(response);

    }
}
