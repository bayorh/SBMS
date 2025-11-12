using SBMS.src.Dtos;

namespace SBMS.src.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
        Task<(bool valid, string reason)> ValidateTokenAsync(string serviceId, string tokenId);
    }
}
