using FluentResults;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Application.Common.Interfaces.Services;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
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
    private readonly ISubscriptionPreferenceRepository _subscriptionPreferenceRepository;
    private readonly IUserRepository _userRepository;
    public SubscriptionService(IMercadoPagoService mercadoPagoService, IUserRepository userRepository, ISubscriptionPreferenceRepository subscriptionPreferenceRepository)
    {
        _mercadoPagoService = mercadoPagoService;
        _userRepository = userRepository;
        _subscriptionPreferenceRepository = subscriptionPreferenceRepository;
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

    public async Task<Result<string>> CreateSubscriptionPreference(CreateSubscriptionPreferenceRequest request)
    {
        var actualPref = await _subscriptionPreferenceRepository.GetAllAsync();
        if (actualPref.Count > 0) return Result.Fail("The system only can have a one subscription preference");

        var preferenceId = await _mercadoPagoService.CreatePreference(request.Title, request.Description, request.Price);

        var subPref = new SubscriptionPreference(preferenceId, request.Title, request.Description, request.Price);

        await _subscriptionPreferenceRepository.AddAsync(subPref);

        return Result.Ok(preferenceId);
    }

    public async Task<Result> UpdateSubscriptionPreference(UpdateSubscriptionPreferenceRequest request)
    {
        var actualPref = await _subscriptionPreferenceRepository.GetAllAsync();
        if (actualPref.Count == 0) return Result.Fail("There are 0 subscriptions preferences in db");

        var firstSubPref = actualPref.FirstOrDefault();
        if (firstSubPref is null) return Result.Fail("There are 0 subscriptions preferences in db");

        try
        {
            await _mercadoPagoService.UpdatePreference(firstSubPref.Id, request.Title, request.Description, request.Price);
        }
        catch(Exception ex)
        {
            return Result.Fail($"An error has ocurred with Mercado Pago API: {ex.Message}");
        }

        firstSubPref.Title = request.Title;
        firstSubPref.Description = request.Description;
        firstSubPref.Price = request.Price;
        await _subscriptionPreferenceRepository.UpdateAsync(firstSubPref);

        return Result.Ok();
    }

    public async Task<Result<GetSubscriptionPreferenceResponse>> GetSubscriptionPreference()
    {
        var subPref = await _subscriptionPreferenceRepository.GetAllAsync();

        if (subPref.Count == 0) return Result.Fail("There are 0 subscriptions preferences in db");

        var firstSubPref = subPref.FirstOrDefault();

        if (firstSubPref is null) return Result.Fail("There are 0 subscriptions preferences in db");

        var response = new GetSubscriptionPreferenceResponse(firstSubPref.Id, firstSubPref.Title, firstSubPref.Description, firstSubPref.Price);

        return Result.Ok(response);
    }

    public async Task<Result> RemoveActualSubscription()
    {
        var subPref = await _subscriptionPreferenceRepository.GetAllAsync();

        if (subPref.Count == 0) return Result.Fail("There are 0 subscriptions preferences in db");

        var firstSubPref = subPref.FirstOrDefault();

        if (firstSubPref is null) return Result.Fail("There are 0 subscriptions preferences in db");

        await _subscriptionPreferenceRepository.DeleteAsync(firstSubPref);

        return Result.Ok();
    }
}
