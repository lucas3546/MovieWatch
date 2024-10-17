using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Client.User;
using MercadoPago.Config;
using MercadoPago.Resource;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    private readonly IConfiguration _configuration;
    public MercadoPagoService(IConfiguration configuration)
    {
        MercadoPagoConfig.AccessToken = configuration["MercadoPagoAccessConfig:AccessToken"];
    }
    public async Task<(string status, string payerEmail)> GetPaymentInformation(long PaymentId)
    {
        var client = new PaymentClient();

        var paymentInfo = await client.GetAsync(PaymentId);

        return (paymentInfo.Status, paymentInfo.Payer.Email);
    }

    public async Task<string> CreatePreference(string Title, string Description, decimal Price)
    {
        var client = new PreferenceClient();

        var item = new PreferenceItemRequest
        {
            Id = "MovieWatch Subscription",
            Title = Title,
            Description = Description,
            CurrencyId = "ARS",
            Quantity = 1,
            UnitPrice = Price
        };

        var back_urls = new PreferenceBackUrlsRequest
        {
            Success = "https://localhost:3000/payment/success",
            Pending = "https://localhost:3000/payment/pending",
            Failure = "https://localhost:3000/payment/failure",
        };


        var preference = new PreferenceRequest
        {
            Items = new List<PreferenceItemRequest> { item },
            BackUrls = back_urls,
            AutoReturn = "approved",
        };

        var result = await client.CreateAsync(preference);

        return result.Id;
        
    }

    public async Task UpdatePreference(string preferenceId, string Title, string Description, decimal Price)
    {
        var client = new PreferenceClient();

        var item = new PreferenceItemRequest
        {
            Id = "MovieWatch Subscription",
            Title = Title,
            Description = Description,
            CurrencyId = "ARS",
            Quantity = 1,
            UnitPrice = Price
        };

        var back_urls = new PreferenceBackUrlsRequest
        {
            Success = "https://localhost:3000/payment/success",
            Pending = "https://localhost:3000/payment/pending",
            Failure = "https://localhost:3000/payment/failure",
        };


        var preference = new PreferenceRequest
        {
            Items = new List<PreferenceItemRequest> { item },
            BackUrls = back_urls,
            AutoReturn = "approved",
        };

        await client.UpdateAsync(preferenceId, preference);

    }


    public async Task<(string id, string currencyId, decimal? price)> GetSubscriptionPreference(string preferenceId)
    {
        var client = new PreferenceClient();

        var result = await client.GetAsync(preferenceId);

        var resultItems = result.Items.FirstOrDefault();

        return (result.Id, resultItems.CurrencyId, resultItems.UnitPrice);
    }

}
