namespace SBMS.src.Entitiies
{
    public class Subscription :BaseEntity
    { 
        public string Action { get; set; } = string.Empty;
        public string ServiceId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime LastActionDate { get; init; } = DateTime.UtcNow;
        public string TokenId { get; set; } = string.Empty;
    }
    
}
