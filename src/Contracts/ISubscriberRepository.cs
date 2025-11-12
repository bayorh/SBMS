using SBMS.src.Entitiies;

namespace SBMS.src.Contracts
{
    public interface ISubscriberRepository
    {
        Task<Subscriber> GetSubsciberBy(string serviceId, string phoneNumber);
        Task<Subscriber> GetSubsciberBy(string serviceId);
        Task<Subscriber> AddSubscriber(Subscriber subsciber);
        Task<Subscription> AddSubscription(Subscription subscription);

    }
}
