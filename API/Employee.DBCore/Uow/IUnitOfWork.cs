using Employee.DBCore.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.DBCore.Uow
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        List<TEntity> SpRepository<TEntity>(string spName, params object[] parameters) where TEntity : class;

    }
}
