using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Errors;
public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    { 
    }
}
