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

            if (context.Subscribers.Any())
            {
                return;   
            }

          
            var service = new Service
            {
                Id = "TestSvc1"
            };

           
            var serviceRecord = new Subscriber
            {
                ServiceId = service.Id, 
                PasswordHash = "password123", 
                PhoneNumber = "0000000000",
                IsSubscribed = false,
                LastActionAt = DateTime.UtcNow
            };

            context.Services.Add(service);
            context.Subscribers.Add(serviceRecord);
            context.SaveChanges();
        }
    }
}
