using Microsoft.EntityFrameworkCore;
using SBMS.src.Contracts;
using SBMS.src.Data;
using SBMS.src.Entitiies;
using System.Threading.Tasks;

namespace SBMS.src.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceToken> CreateServiceToken(ServiceToken serviceToken)
        {
            await _context.ServiceTokens.AddAsync(serviceToken);
            return serviceToken;
        }

        public async Task<ServiceToken> GetServiceTokenBy(string serviceId)
        {
            return await _context.ServiceTokens
                .FirstOrDefaultAsync(st => st.ServiceId == serviceId && st.ExpiresAt > System.DateTime.UtcNow);
        }
    }
}