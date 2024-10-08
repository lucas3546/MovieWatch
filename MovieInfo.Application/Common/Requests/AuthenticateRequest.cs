using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Requests
{
    public class AuthenticateRequest
    {
        //[MinLength(3), MaxLength(15)]
        public string Email { get; set; }
        //[MinLength(8), MaxLength(16)]
        public string Password { get; set; }
    }
}
