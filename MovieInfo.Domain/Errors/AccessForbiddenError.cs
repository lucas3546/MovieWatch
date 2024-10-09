using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Errors
{
    public class AccessForbiddenError : Error
    {
        public AccessForbiddenError(string message) : base(message)
        {
        }
    }
}

