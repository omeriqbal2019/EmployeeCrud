using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.DBCore.Context.EFContext
{
    public interface IDatabaseContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool confirmAllTransactions, CancellationToken cancellationToken);
        int SaveChanges();
        int SaveChanges(bool confirmAllTransactions);
        void Dispose();
    }
}
