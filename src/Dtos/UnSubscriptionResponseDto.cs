namespace SBMS.src.Dtos
{
    public record UnSubscriptionResponseDto(
        string Message,
        bool Success = false) : BaseResponse(Message, Success);
}
