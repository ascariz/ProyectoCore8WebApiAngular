using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Interface.Dominio.Base
{
    public interface IRepository
    {
        void Save();
        Task<int> SaveAsync();
        void Dispose();
    }

    public interface IRepository<T> : IRepository where T : class { }

    public interface IBaseRepository<T, TDb> : IRepository<T> where T : class
    {
        T Add(T t);
        // T AddWithLogs(T t, Guid? userId, Type primaryEntityType);
        T AddAsNoTracking(T t);
        Task<T> AddAsync(T t);
        IEnumerable<T> AddRange(IEnumerable<T> t);
        int Count(Expression<Func<T, bool>> match);

        Task<int> CountAsync();
        void Delete(params object[] id);
        Task<int> DeleteAsync(params object[] id);
        T Find(Expression<Func<T, bool>> match);
        IQueryable<T> FindAll(Expression<Func<T, bool>> match);
        Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        T Get(params object[] id);
        IQueryable<T> GetAllCache();
        IQueryable<T> GetAll();
        Task<IQueryable<T>> GetAllAsync();
        Task<List<T>> GetAllListAsync();
        IQueryable<T> GetAllIncluding(params Expression<Func<TDb, object>>[] includeProperties);
        IQueryable<T> GetPagedIncludeOrderby(Expression<Func<T, bool>> match, int page, Expression<Func<TDb, object>> sorting, Expression<Func<TDb, object>> sorting2, int pageSize, params Expression<Func<TDb, object>>[] includeProperties);
        IQueryable<T> GetPagedInclude(Expression<Func<T, bool>> match, int page, int pageSize, params Expression<Func<TDb, object>>[] includeProperties);
        IQueryable<T> GetPaged(Expression<Func<T, bool>> match, int page, int pageSize);
        Task<T> GetAsync(params object[] id);
        T Update(T t, params object[] key);
        // T UpdateWithLogs(T t, Guid? userId, Type primaryEntityType, params object[] key);
        Task<T> UpdateAsync(T t, params object[] key);
       // QueryResult<T> GetPaged(DataSourceLoadOptionsMio dataSourceLoadOptions);
    }
}
