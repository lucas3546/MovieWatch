using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests;
public record ResetPasswordRequest
{
    public required string ResetJwt { get; init; }
    [MinLength(4), MaxLength(50)]
    public required string NewPassword { get; init; }
}
