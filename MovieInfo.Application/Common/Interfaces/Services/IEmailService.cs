using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IEmailService
{
    Task<bool> SendEmailAsync(string receiverEmail, string subject, string messageBody);
}
