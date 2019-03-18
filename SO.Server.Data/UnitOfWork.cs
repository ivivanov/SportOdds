using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SO.Server.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }


        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        int IUnitOfWork.Commit()
        {
            throw new System.NotImplementedException();
        }
    }
}
