using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Errors;
public class PaymentInformationError : Error
{
    public PaymentInformationError(string message) : base(message)
    {
    }
}
