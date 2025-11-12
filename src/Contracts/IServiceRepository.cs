using SBMS.src.Entitiies;

namespace SBMS.src.Contracts
{
    public interface IServiceRepository
    {
        Task<ServiceToken> GetServiceTokenBy(string serviceId);
        Task<ServiceToken> CreateServiceToken(ServiceToken serviceToken);

    }
}
