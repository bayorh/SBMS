using SBMS.src.Contracts;
using SBMS.src.Data;
using System.Threading.Tasks;

namespace SBMS.src.Servicess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}