using Employee.DBCore.Context.EFContext;
using Employee.DBCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Employee.DBCore.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T>
         where T : class
    {
        public IDatabaseContext Context { get; }
        private readonly DbSet<T> _dbSet;

        public GenericRepository(IDatabaseContext context)
        {
            Context = context;
            _dbSet = context.Set<T>();
        }

        public virtual EntityState Add(T entity)
        {

            return _dbSet.Add(entity).State;
        }

        public T Get<TKey>(TKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetAsync<TKey>(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Get(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include)
        {
            return FindBy(predicate).Include(include);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return _dbSet.Skip(pageSize).Take(pageCount);
        }

        public IQueryable<T> GetAll(string include)
        {
            return _dbSet.Include(include);
        }

        public IQueryable<T> RawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters);
        }

        public IQueryable<T> GetAll(string include, string include2)
        {
            return _dbSet.Include(include).Include(include2);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public virtual EntityState SoftDelete(T entity)
        {
            entity.GetType().GetProperty("IsActive")?.SetValue(entity, false);
            return _dbSet.Update(entity).State;
        }

        public virtual EntityState HardDelete(T entity)
        {
            return this._dbSet.Remove(entity).State;
        }
        public virtual EntityState Update(T entity)
        {
            return _dbSet.Update(entity).State;
        }
    }
}
