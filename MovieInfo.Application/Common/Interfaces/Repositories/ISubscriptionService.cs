using FluentResults;
using MovieInfo.Application.Common.Requests;
using MovieInfo.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Application.Common.Interfaces.Repositories;
public interface ISubscriptionService
{
    public Task<Result> AddSubscriptionToUserFromPayment(long PaymentId);
    Task<Result<GetSubscriptionPreferenceResponse>> GetSubscriptionPreference();
    Task<Result<string>> CreateSubscriptionPreference(CreateSubscriptionPreferenceRequest request);
    Task<Result> UpdateSubscriptionPreference(UpdateSubscriptionPreferenceRequest request);
    Task<Result> RemoveActualSubscription();
    Task<Result> InvalidateSubscriptionToUser(string userName);
}
