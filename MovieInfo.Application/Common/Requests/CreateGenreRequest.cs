﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public record CreateGenreRequest
    {
        [MinLength(4),MaxLength(30)]
        public required string Name { get; set; }
    }
}
