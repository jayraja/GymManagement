using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);

    Task<Subscription?> GetByIdAsync(Guid subscriptionId);

    Task<bool> ExistsAsync(Guid id);

    Task UpdateAsync(Subscription subscription);
    Task RemoveSubscriptionAsync(Subscription subscription);

}