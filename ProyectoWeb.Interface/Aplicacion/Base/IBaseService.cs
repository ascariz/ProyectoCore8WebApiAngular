using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Interface.Aplicacion.Base
{
    public interface IService { }
    public interface IService<T> : IService where T : class { }
    public interface IBaseService<TDto> : IService<TDto> where TDto : class
    {
        int GetCount(Expression<Func<TDto, bool>> match);

        LoadResult GetAllCache(DataSourceLoadOptions loadOptions);
        LoadResult GetAll(DataSourceLoadOptions loadOptions);
        List<TDto> GetAll();
        IList<TDto> GetAllCache();
        TDto Add(TDto item);
        TDto AddAsNoTracking(TDto item);
        List<TDto> AddRange(List<TDto> entity);
        // TDto PutWithLogs(TDto item, Guid? userId, Type primaryEntityType);
        TDto Find(Expression<Func<TDto, bool>> match);
        IQueryable<TDto> FindAll(Expression<Func<TDto, bool>> match);
        TDto Put(TDto item, params object[] key);
        void Delete(string item);
        void Delete(Guid item);
        void Delete(int id);
        TDto Get(Guid key);
        TDto Get(int key);
        TDto Get(string key);
        Task<TDto> GetAsync(Guid key);
        Task<TDto> AddAsync(TDto item);
        Task<List<TDto>> GetAllAsync();
        Task<List<TDto>> GetAllListCacheAsync();
    }
}

