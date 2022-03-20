using Employee.DBCore.Context.EFContext;
using Employee.DBCore.Factory;
using Employee.DBCore.Repositories.Base;
using Employee.DBCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.DBCore.Uow
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDatabaseContext _dbContext;
        private readonly DatabaseContext _dBConnectionContext;

        private Dictionary<Type, object> _repos;

        public UnitOfWork(IContextFactory contextFactory, DatabaseContext dBConnectionContext)
        {
            _dbContext = contextFactory.DbContext;
            _dBConnectionContext = dBConnectionContext;
        }
        public List<TEntity> SpRepository<TEntity>(string spName, params object[] parameters) where TEntity : class
        {
            return _dBConnectionContext.Set<TEntity>().FromSqlRaw(spName, parameters).ToList();
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (_repos == null)
            {
                _repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repos.ContainsKey(type))
            {
                _repos[type] = new GenericRepository<TEntity>(_dbContext);
            }

            return (IGenericRepository<TEntity>)_repos[type];
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
