using System.ComponentModel.DataAnnotations.Schema;

namespace SBMS.src.Entitiies
{
    public class Subscriber : BaseEntity
    {
        public string ServiceId { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsSubscribed { get; set; } = false;
        public DateTime LastActionAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}