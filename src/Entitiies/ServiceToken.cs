using System.ComponentModel.DataAnnotations.Schema;

namespace SBMS.src.Entitiies
{
    public class ServiceToken : BaseEntity
    {
        public string ServiceId { get; set; } = null!;
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}