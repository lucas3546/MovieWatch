using FluentResults;
using MovieInfo.Application.Common.Helpers;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Models;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services
{

    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IFileService _fileService;
        public EpisodeService(IEpisodeRepository episodeRepository, ISeasonRepository seasonRepository, IFileService fileService)
        {
            _episodeRepository = episodeRepository;
            _seasonRepository = seasonRepository;
            _fileService = fileService;
        }
        public async Task<Result<int>> AddEpisodeToSeason(CreateEpisodeRequest request)
        {
            var season = await _seasonRepository.GetByIdAsync(request.SeasonId);
            if (season is null) return Result.Fail(new NotFoundError($"Season not found"));

            string episodeMediaType = MediaHelper.GetMediaType(request.EpisodeVideo);
            if (!episodeMediaType.Equals("video") || episodeMediaType.Equals("unknow")) return Result.Fail(new UnsupportedMediaTypeError($"Only videos are allowed in EpisodeVideo."));

            Media episodeVideo;
            try
            {
                var (videoFileName, videoFileType, isVideoPublic) = await _fileService.SaveFileAsync(request.EpisodeVideo, false);
                episodeVideo = new Media(videoFileName, videoFileType, isVideoPublic);
            }
            catch (Exception ex)
            {
                return Result.Fail(new FileSaveError("File save error", ex.Message));
            }

            var episode = new Episode
            {
                Title = request.Title,
                Media = episodeVideo,
                Season = season,
            };

            await _episodeRepository.AddAsync(episode);

            return Result.Ok(episode.Id);
        }
        public async Task<Result<IEnumerable<GetEpisodeFromSeasonResponse>>> GetEpisodeFromSeasonAsync(int id)
        {
            var season = await _seasonRepository.GetSeasonByIdWithEpisode(id);

            if (season == null) return Result.Fail(new NotFoundError("Season not found"));

            var resp = season.Episodes.Select(e => new GetEpisodeFromSeasonResponse(e.Id, e.Title));

            return Result.Ok(resp);

        }

        public async Task<Result<GetEpisodeByIdResponse>> GetEpisodeById(int Id)
        {
            var episode = await _episodeRepository.GetEpisodeByIdWithMedia(Id);
            if (episode is null) return Result.Fail(new NotFoundError($"Episode not found"));

            var response = new GetEpisodeByIdResponse(episode.Id, episode.Title, new MediaModel(episode.Media.FileName, episode.Media.FileExtension, episode.Media.IsPublic));

            return Result.Ok(response);

        }
    }
}
