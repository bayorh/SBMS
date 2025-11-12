namespace SBMS.src.Entitiies
{
    public class Service : BaseEntity
    {
        public ICollection<Subscriber> Subscribers { get; set; }
    }
}