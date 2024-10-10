using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;
using MovieInfo.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Services;
public class MercadoPagoService : IMercadoPagoService
{
    public async Task<(string status, string payerEmail)> GetPaymentInformation(long PaymentId)
    {
        var client = new PaymentClient();

        var paymentInfo = await client.GetAsync(PaymentId);

        return (paymentInfo.Status, paymentInfo.Payer.Email);
    }
}
