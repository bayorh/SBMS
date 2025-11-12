using SBMS.src.Data;
using SBMS.src.Entitiies;
using System.Linq;

namespace SBMS.src
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any subscribers.
            if (context.Subscribers.Any())
            {
                return;   // DB has been seeded
            }

            // 1. Create the parent Service record.
            // Its ID must match the business key we use for the service.
            var service = new Service
            {
                Id = "TestSvc1"
            };

            // 2. Create a subscriber record that also defines the service credentials
            var serviceRecord = new Subscriber
            {
                ServiceId = service.Id, // Set the foreign key to the Service's Id
                PasswordHash = "password123", // Storing plain text as per previous implementation notes
                PhoneNumber = "0000000000", // Dummy phone number for the service record
                IsSubscribed = false,
                LastActionAt = System.DateTime.UtcNow
            };

            context.Services.Add(service);
            context.Subscribers.Add(serviceRecord);
            context.SaveChanges();
        }
    }
}
