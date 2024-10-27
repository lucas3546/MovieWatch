using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests;
public record CreateEpisodeRequest
{
    [MinLength(5), MaxLength(30)]
    public string Title { get; set; }
    public int SeasonId { get; set; }
    public IFormFile EpisodeVideo { get; set; }

}
