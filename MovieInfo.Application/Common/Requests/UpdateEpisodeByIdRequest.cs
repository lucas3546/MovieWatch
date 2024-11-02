using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public class UpdateEpisodeByIdRequest
    {
        public string Title { get; set; }
        public IFormFile Media { get; set; }
    }
}
