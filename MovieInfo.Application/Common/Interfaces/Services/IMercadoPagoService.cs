using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Services;
public interface IMercadoPagoService
{
    Task<(string status, string payerEmail)> GetPaymentInformation(long PaymentId);

    Task<string> CreatePreference(string Title, string Description, decimal Price);

    Task UpdatePreference(string preferenceId, string Title, string Description, decimal Price);

    Task<(string id, string currencyId, decimal? price)> GetSubscriptionPreference(string preferenceId);
}
