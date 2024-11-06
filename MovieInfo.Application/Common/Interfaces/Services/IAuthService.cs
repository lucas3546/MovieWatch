using FluentResults;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IAuthService
{
    Task<Result<int>> RegisterAsync(RegisterUserRequest request);
    Task<Result<AuthenticateResponse>> Authenticate(AuthenticateRequest authenticateRequest);
    Task<Result<RefreshTokenResponse>> RefreshToken(string refreshToken, string userName);
    Task<Result> RequestResetPassword(string Email);
    Task<Result> ResetPassword(ResetPasswordRequest request);

}
