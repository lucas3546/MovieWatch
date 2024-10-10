using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Domain.Entities;
using MovieInfo.Domain.Enums;
using MovieInfo.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Services;
public class SubscriptionService :  ISubscriptionService
{
    private readonly IMercadoPagoService _mercadoPagoService;
    private readonly IUserRepository _userRepository;
    public SubscriptionService(IMercadoPagoService mercadoPagoService, IUserRepository userRepository)
    {
        _mercadoPagoService = mercadoPagoService;
        _userRepository = userRepository;
    }

    public async Task<Result> AddSubscriptionToUserFromPayment(long PaymentId)
    {
        string paymentStatus;
        string payerEmail;

        try
        {
            var (status, email) = await _mercadoPagoService.GetPaymentInformation(PaymentId);
            paymentStatus = status;
            payerEmail = email;
        }
        catch(Exception ex)
        {
            return Result.Fail(new PaymentInformationError($"Error when trying to obtain payment information, details: {ex.Message}"));
        }

        if (!paymentStatus.Equals("approved")) return Result.Fail(new PaymentInformationError($"Error, the payment is not approved, the payment is {paymentStatus}"));

        var user = await _userRepository.GetUserWithRoleAndSubscriptionByEmailAsync(payerEmail);
        if (user is null) return Result.Fail(new NotFoundError("User not found ;("));

        var newSubscription = new Subscription { StartDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(30), State = Domain.Enums.SubscriptionState.Active};

        user.Subscription = newSubscription;

        return Result.Ok();
    }
}
