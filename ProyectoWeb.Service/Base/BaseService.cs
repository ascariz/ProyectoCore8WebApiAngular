using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProyectoWeb.Interface.Aplicacion.Base;
using System.Linq.Expressions;
using System.Text.Json;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Text.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Interface.Dominio.Base;

namespace ProyectoWeb.Service.Base
{
    public class BaseService<TDto, T, R> : IBaseService<TDto>
       where TDto : class
       where R : IBaseRepository<TDto, T>
    {
        protected readonly R _repository;
        private readonly IMemoryCache _memoryCache;
        public BaseService(R repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }
        public LoadResult GetAllCache(DataSourceLoadOptions loadOptions)
        {
            string cacheKey = JsonSerializer.Serialize<DataSourceLoadOptions>(loadOptions);
            List<TDto> dtos;

            if (!_memoryCache.TryGetValue(cacheKey, out dtos))
            {
                dtos = _repository.GetAll().ToList();

                _memoryCache.Set(cacheKey, dtos, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2)));
            }
            return DataSourceLoader.Load(dtos, loadOptions);


        }
        public LoadResult GetAll(DataSourceLoadOptions loadOptions)
        {

            List<TDto> dtos = _repository.GetAll().ToList();

            return DataSourceLoader.Load(dtos, loadOptions);


        }


        public int GetCount(Expression<Func<TDto, bool>> match)
        {
            return _repository.Count(match);

        }
        public IList<TDto> GetAllCache()
        {
            // return _repository.GetAll().ToList();

            string cacheKey = $"GetAll{typeof(TDto)}";
            List<TDto> dtos;

            if (!_memoryCache.TryGetValue(cacheKey, out dtos))
            {
                dtos = _repository.GetAll().ToList();

                _memoryCache.Set(cacheKey, dtos, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2)));
            }
            return dtos;

        }
        public List<TDto> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public async Task<List<TDto>> GetAllAsync()
        {
            var queryableResult = await _repository.GetAllAsync();
            return queryableResult.ToList();
        }
        public async Task<List<TDto>> GetAllListCacheAsync()
        {
            string cacheKey = $"{nameof(TDto)}GetAllCacheAsync";

            if (!_memoryCache.TryGetValue(cacheKey, out List<TDto> dtos))
            {
                dtos = await _repository.GetAllListAsync(); // No usamos ToListAsync porque GetAllAsync ya devuelve una lista

                _memoryCache.Set(cacheKey, dtos, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(2)));
            }

            return dtos;
        }


        public TDto GetById(object id)
        {
            return _repository.Get(id);
        }
        public TDto Get(Guid key)
        {
            return _repository.Get(key);
        }
        public TDto Get(int key)
        {
            return _repository.Get(key);
        }
        public TDto Get(string key)
        {
            return _repository.Get(key);
        }
        public TDto Add(TDto entity)
        {
            return _repository.Add(entity);
        }

        public TDto AddAsNoTracking(TDto entity)
        {
            return _repository.AddAsNoTracking(entity);
        }

        public List<TDto> AddRange(List<TDto> entity)
        {
            return _repository.AddRange(entity).ToList();
        }
        public TDto Put(TDto entity, params object[] key)
        {
            return _repository.Update(entity, key);
        }
        public void Delete(string item)
        {
            _repository.Delete(item);
        }
        public void Delete(Guid item)
        {
            _repository.Delete(item);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public virtual TDto Find(Expression<Func<TDto, bool>> match)
        {
            return _repository.Find(match);
        }

        public IQueryable<TDto> FindAll(Expression<Func<TDto, bool>> match)
        {
            return _repository.FindAll(match);
        }


        public async Task<TDto> GetAsync(Guid key)
        {
            return await _repository.GetAsync(key);
        }

        public Task<TDto> AddAsync(TDto item)
        {
            return _repository.AddAsync(item);
        }
    }
}
