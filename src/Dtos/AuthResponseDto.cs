namespace SBMS.src.Dtos
{
    public record AuthResponseDto(
        string Message,
        bool Success = false,
        string? TokenId = null,
        DateTime? ExpiresAt = null): BaseResponse(Message, Success);

}