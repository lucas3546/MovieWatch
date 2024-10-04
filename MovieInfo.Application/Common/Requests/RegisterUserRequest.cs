using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests;

public record RegisterUserRequest
{
    [MaxLength(30)]
    public required string UserName { get; set; }
    [MaxLength(30)]
    [MinLength(5)]
    public required string Password { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
};