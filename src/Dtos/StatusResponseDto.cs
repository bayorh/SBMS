using System;

namespace SBMS.src.Dtos
{
    public record StatusResponseDto(
        string Message,
        bool Success,
        bool? IsSubscribed,
        DateTime? LastActionDate) : BaseResponse(Message, Success);
}
