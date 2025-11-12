namespace SBMS.src.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
