using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Employee.DBCore.Repositories.Interfaces
{
 
    public interface IGenericRepository<T>
    {

        EntityState Add(T entity);  
        EntityState Update(T entity);
        T Get<TKey>(TKey id);
        Task<T> GetAsync<TKey>(TKey id);
        T Get(params object[] keyValues);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(int page, int pageCount);
        IQueryable<T> GetAll(string include);
        IQueryable<T> RawSql(string sql, params object[] parameters);
        IQueryable<T> GetAll(string include, string include2);
        EntityState SoftDelete(T entity);
        EntityState HardDelete(T entity);
        bool Exists(Expression<Func<T, bool>> predicate);


    }
}
