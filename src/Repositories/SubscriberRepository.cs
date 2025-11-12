using Microsoft.EntityFrameworkCore;
using SBMS.src.Contracts;
using SBMS.src.Data;
using SBMS.src.Entitiies;
using System.Threading.Tasks;

namespace SBMS.src.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly AppDbContext _context;

        public SubscriberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subscriber> AddSubscriber(Subscriber subscriber)
        {
            await _context.Subscribers.AddAsync(subscriber);
            return subscriber;
        }

        public async Task<Subscription> AddSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            return subscription;
        }

        public async Task<Subscriber> GetSubsciberBy(string serviceId, string phoneNumber)
        {
            return await _context.Subscribers
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId && s.PhoneNumber == phoneNumber);
        }

        public async Task<Subscriber> GetSubsciberBy(string serviceId)
        {
            // This implementation is based on the flawed data model.
            // It assumes any subscriber record can be used to represent the service.
            return await _context.Subscribers
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }
    }
}