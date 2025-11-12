namespace SBMS.src.Dtos
{
    public record SubscriptionResponseDto(
        string Message,
        bool Success= false,
        string? SubscriptionID = null) : BaseResponse(Message,Success);
}
 