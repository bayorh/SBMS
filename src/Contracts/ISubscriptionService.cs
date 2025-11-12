using SBMS.src.Dtos;

namespace SBMS.src.Contracts
{
    public interface ISubscriptionService
    {
        Task<SubscriptionResponseDto> Subscribe(SubscriptionRequestDto request);
        Task<UnSubscriptionResponseDto> UnSubscribe(UnSubscriptionRequestDto request);
        Task<StatusResponseDto> CheckStatus(StatusRequestDto request);
    }

}
