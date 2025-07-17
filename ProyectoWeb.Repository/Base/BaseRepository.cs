using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Data;
using ProyectoWeb.Interface.Dominio.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AppToContext = ProyectoWeb.CrossCutting.Mappers.DtoToEntry;
using ContextToApp = ProyectoWeb.CrossCutting.Mappers.EntryToDto;

using Microsoft.Extensions.Caching.Memory;
using DevExtreme.AspNet.Data;
using ProyectoWeb.Repository.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace ProyectoWeb.Repository.Base
{
    public abstract class BaseRepository<TApp, TDb> : IBaseRepository<TApp, TDb>
       where TApp : class
       where TDb : class
    {
        protected ApplicationDbContext _context;
        protected Func<TDb, object> _defaultOrderBy;
        protected bool _orderByDescending;
        protected Expression<Func<TDb, object>>[] _defaultIncludeProperties;
        private readonly IMemoryCache _memoryCache;

        public BaseRepository(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }


        public BaseRepository(IMemoryCache memoryCache, ApplicationDbContext context, Func<TDb, object> defaultOrderBy)
        {
            _context = context;
            _defaultOrderBy = defaultOrderBy;
            _memoryCache = memoryCache;
        }
        public BaseRepository(IMemoryCache memoryCache, ApplicationDbContext context, Func<TDb, object> defaultOrderBy, bool orderByDescending)
        {
            _context = context;
            _defaultOrderBy = defaultOrderBy;
            _orderByDescending = orderByDescending;
            _memoryCache = memoryCache;
        }
        public BaseRepository(IMemoryCache memoryCache, ApplicationDbContext context, Func<TDb, object> defaultOrderBy, bool orderByDescending, params Expression<Func<TDb, object>>[] defaultIncludeProperties)
        {
            _context = context;
            _defaultOrderBy = defaultOrderBy;
            _orderByDescending = orderByDescending;
            _defaultIncludeProperties = defaultIncludeProperties;
            _memoryCache = memoryCache;
        }

        #region Private
        /// <summary>
        /// Valida las propiedades de tipo string en una entidad.
        /// Verifica que los strings no sean nulos ni vacíos, y que cumplan 
        /// con las restricciones de longitud especificadas por el atributo [Column].
        /// Lanza una excepción si alguna validación falla.
        /// </summary>
        /// <typeparam name="T">El tipo de la entidad a validar.</typeparam>
        /// <param name="entity">La instancia de la entidad a validar.</param>
        /// <exception cref="ArgumentException">
        /// Se lanza si alguna propiedad string es inválida.
        /// </exception>
        private void ValidateEntity<T>(T entity) // Método genérico que valida una entidad de cualquier tipo.
        {
            // Obtiene todas las propiedades de la entidad que:
            // - Son de tipo string.
            // - Se pueden leer (get) y escribir (set).
            var properties = typeof(T).GetProperties()
                                      .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            // Recorre cada una de las propiedades seleccionadas.
            foreach (var property in properties)
            {
                // Obtiene el valor actual de la propiedad para la entidad pasada como parámetro.
                var value = property.GetValue(entity) as string;

                // Validación solo si el valor NO es null o Vacio
                if (value == null || value == "")
                {
                    continue; // No se realiza ninguna validación para null o cadenas vacías.
                }
                //if (value != null)
                //{
                //    // Primera validación: Verifica que el valor de la propiedad no esté vacío o compuesto únicamente por espacios.
                //    if (string.IsNullOrWhiteSpace(value))
                //    {
                //        // Si no se cumple la validación, lanza una excepción indicando el nombre de la propiedad.
                //        throw new ArgumentException($"La propiedad {property.Name} no puede estar vacía.");
                //    }

                // Obtiene el atributo personalizado [Column] asociado a la propiedad, si existe.
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null && columnAttribute.TypeName.StartsWith("nvarchar"))
                {
                    // Extrae el tamaño máximo del string a partir del valor de TypeName (por ejemplo, "nvarchar(10)").
                    var maxLengthString = columnAttribute.TypeName.Replace("nvarchar(", "").Replace(")", "");

                    // Intenta convertir el tamaño máximo de string (nvarchar(X)) a un número entero.
                    if (int.TryParse(maxLengthString, out int maxLength))
                    {
                        // Segunda validación: Comprueba si la longitud del string excede el límite definido.
                        if (value.Length > maxLength)
                        {
                            // Si no se cumple la validación, lanza una excepción indicando el nombre de la propiedad y el límite.
                            throw new ArgumentException($"La propiedad {property.Name} excede el límite de {maxLength} de {value.Length} valor {value} caracteres.");
                        }
                    }
                }
                // Validar el atributo [MaxLength(255)]
                var maxLengthAttribute = property.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLengthAttribute != null && value.Length > maxLengthAttribute.Length)
                {
                    throw new ArgumentException($"La propiedad {property.Name} excede el límite de {maxLengthAttribute.Length} caracteres definido por [MaxLength].");
                }
                // }
            }
        }


        private Expression<Func<TDb, bool>> ConvertExpressionToDbType(Expression<Func<TApp, bool>> expression)
        {
            var param = Expression.Parameter(typeof(TDb));
            var fresult = new CustomExpressionVisitor<TApp, TDb>(param).Visit(expression.Body);
            Expression<Func<TDb, bool>> lambda = Expression.Lambda<Func<TDb, bool>>(fresult, param);

            return lambda;
        }

        #endregion
        public virtual IQueryable<TApp> GetAll()
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            if (_defaultIncludeProperties != null && _defaultIncludeProperties.Any())
            {
                data = GetAllIncluding(data, _defaultIncludeProperties);
            }

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data);

            return result.AsQueryable();
        }


        public virtual IQueryable<TApp> GetAllCache()
        {
            string cacheKey = $"{nameof(TApp)}GetAllCache";
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            //IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            IEnumerable<TApp> dtos;

            if (!_memoryCache.TryGetValue(cacheKey, out dtos))
            {
                IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();
                dtos = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data);
                _memoryCache.Set(cacheKey, dtos, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2)));
            }

            //var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data);

            return dtos.AsQueryable();
        }
        public IQueryable<TApp> GetAllIncluding(params Expression<Func<TDb, object>>[] includeProperties)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var queryable = _context.Set<TDb>().AsNoTracking().AsQueryable();

            foreach (Expression<Func<TDb, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TDb, object>(includeProperty);
            }
            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(queryable);

            return result.AsQueryable();
            //return queryable;
        }
        public virtual async Task<List<TApp>> GetAllListAsync()
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            if (_defaultIncludeProperties != null && _defaultIncludeProperties.Any())
            {
                data = GetAllIncluding(data, _defaultIncludeProperties);
            }

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(await data.ToListAsync());

            return result.ToList(); // Devuelve una lista en lugar de IQueryable
        }
        public virtual async Task<IQueryable<TApp>> GetAllAsync()
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            if (_defaultIncludeProperties != null && _defaultIncludeProperties.Any())
            {
                data = GetAllIncluding(data, _defaultIncludeProperties);
            }

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(await data.ToListAsync());

            return result.AsQueryable();
        }

        public virtual TApp Get(params object[] id)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var result = mapper.GetEntityFromDbObject<TDb, TApp>(_context.Set<TDb>().Find(id));

            return result;
        }

        public virtual async Task<TApp> GetAsync(params object[] id)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var result = mapper.GetEntityFromDbObject<TDb, TApp>(await _context.Set<TDb>().FindAsync(id));

            return result;
        }

        public virtual TApp Add(TApp t)
        {
            TDb dbObject = new AppToContext.GenericMapper<TApp, TDb>().Create(t);

            //Validar la entidad antes de agregarla
            ValidateEntity(dbObject);


            _context.Set<TDb>().Add(dbObject);

            _context.SaveChanges();

            TDb dbObjectResult = _context.Set<TDb>().Local.LastOrDefault();
            //TDb dbObjectResult = _context.Set<TDb>().AsNoTracking().LastOrDefault();
            return new ContextToApp.GenericMapper<TDb, TApp>().Create(dbObjectResult);
        }
        public virtual TApp AddAsNoTracking(TApp t)
        {
            TDb dbObject = new AppToContext.GenericMapper<TApp, TDb>().Create(t);
            _context.Set<TDb>().Add(dbObject);

            _context.SaveChanges();

            TDb dbObjectResult = _context.Set<TDb>()
                             .AsNoTracking() // Añade AsNoTracking aquí
                             .ToList().LastOrDefault();

            //TDb dbObjectResult = _context.Set<TDb>().AsNoTracking().LastOrDefault();
            return new ContextToApp.GenericMapper<TDb, TApp>().Create(dbObjectResult);
        }

        //public virtual TApp AddWithLogs(TApp t, Guid? userId, Type primaryEntityType)
        //{
        //    TDb dbObject = new AppToContext.GenericMapper<TApp, TDb>().Create(t);

        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        _context.Set<TDb>().Add(dbObject);
        //        _context.SaveChangesWithLogs(userId, primaryEntityType);
        //        scope.Complete();
        //    }

        //    TDb dbObjectResult = _context.Set<TDb>().Local.LastOrDefault();
        //    return new ContextToApp.GenericMapper<TDb, TApp>().Create(dbObjectResult);
        //}

        public virtual IEnumerable<TApp> AddRange(IEnumerable<TApp> t)
        {
            IEnumerable<TDb> dbObjects = new AppToContext.GenericMapper<TApp, TDb>().Create(t);
            _context.Set<TDb>().AddRange(dbObjects.ToArray());
            _context.SaveChanges();
            IEnumerable<TDb> dbObjectResult = _context.Set<TDb>().Local.TakeLast(t.Count());
            return new ContextToApp.GenericMapper<TDb, TApp>().Create(dbObjectResult);
        }

        public virtual async Task<TApp> AddAsync(TApp t)
        {
            TDb dbObject = new AppToContext.GenericMapper<TApp, TDb>().Create(t);
            _context.Set<TDb>().Add(dbObject);

            await _context.SaveChangesAsync();

            TDb dbObjectResult = _context.Set<TDb>().Local.FirstOrDefault();
            return new ContextToApp.GenericMapper<TDb, TApp>().Create(dbObjectResult);
        }

        public virtual TApp Find(Expression<Func<TApp, bool>> match)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var lambda = ConvertExpressionToDbType(match);

            var result = mapper.GetEntityFromDbObject<TDb, TApp>(_context.Set<TDb>().AsNoTracking().SingleOrDefault(lambda));

            return result;
        }

        public virtual async Task<TApp> FindAsync(Expression<Func<TApp, bool>> match)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var lambda = ConvertExpressionToDbType(match);

            var result = mapper.GetEntityFromDbObject<TDb, TApp>(await _context.Set<TDb>().SingleOrDefaultAsync(lambda));

            return result;
        }

        public IQueryable<TApp> FindAll(Expression<Func<TApp, bool>> match)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var lambda = ConvertExpressionToDbType(match);

            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            if (_defaultIncludeProperties != null && _defaultIncludeProperties.Any())
            {
                data = GetAllIncluding(data, _defaultIncludeProperties);
            }

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data.Where(lambda));

            return result.AsQueryable();
        }

        public async Task<IQueryable<TApp>> FindAllAsync(Expression<Func<TApp, bool>> match)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));

            var lambda = ConvertExpressionToDbType(match);

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(await _context.Set<TDb>().AsNoTracking().Where(lambda).ToListAsync());

            return result.AsQueryable();
        }

        public virtual void Delete(params object[] id)
        {
            var entity = _context.Set<TDb>().Find(id);
            if (entity != null)
            {
                _context.Set<TDb>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public virtual async Task<int> DeleteAsync(params object[] id)
        {
            var entity = _context.Set<TDb>().Find(id);
            if (entity != null)
            {
                _context.Set<TDb>().Remove(entity);
            }
            return await _context.SaveChangesAsync();
        }

        public virtual TApp Update(TApp t, params object[] key)
        {
            if (t == null)
                return null;
            TDb exist = _context.Set<TDb>().Find(key);
            if (exist != null)
            {
                AppToContext.MapperBase mapper = AppToContext.FactorySwitcher.GetFactoryFor(typeof(TApp));
                TDb updated = mapper.GetDbObjectFromEntity<TApp, TDb>(t);

                _context.Entry(exist).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return t;
        }

        //public virtual TApp UpdateWithLogs(TApp t, Guid? userId, Type primaryEntityType, params object[] key)
        //{
        //    if (t == null)
        //        return null;
        //    TDb exist = _context.Set<TDb>().Find(key);
        //    if (exist != null)
        //    {
        //        AppToContext.MapperBase mapper = AppToContext.FactorySwitcher.GetFactoryFor(typeof(TApp));

        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            TDb updated = mapper.GetDbObjectFromEntity<TApp, TDb>(t);

        //            _context.Entry(exist).CurrentValues.SetValues(updated);
        //            _context.SaveChangesWithLogs(userId, primaryEntityType);
        //            scope.Complete();
        //        }

        //    }
        //    return t;
        //}

        public virtual async Task<TApp> UpdateAsync(TApp t, params object[] key)
        {
            if (t == null)
                return null;
            TDb exist = await _context.Set<TDb>().FindAsync(key);
            if (exist != null)
            {
                AppToContext.MapperBase mapper = AppToContext.FactorySwitcher.GetFactoryFor(typeof(TApp));
                TDb updated = mapper.GetDbObjectFromEntity<TApp, TDb>(t);

                _context.Entry(exist).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return t;
        }


        public int Count(Expression<Func<TApp, bool>> match)
        {
            if (match != null)
            {
                var lambda = ConvertExpressionToDbType(match);
                return _context.Set<TDb>().Where(lambda).Count();
            }
            return _context.Set<TDb>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TDb>().CountAsync();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }



        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IQueryable<TApp> GetPagedIncludeOrderby(Expression<Func<TApp, bool>> match, int page, Expression<Func<TDb, object>> sorting, Expression<Func<TDb, object>> sorting2, int pageSize, params Expression<Func<TDb, object>>[] includeProperties)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));
            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();
            if (sorting != null)
            {
                data = data.OrderBy(sorting).ThenBy(sorting2);
            }
            if (match != null)
            {
                var lambda = ConvertExpressionToDbType(match);
                data = data.Where(lambda);
            }

            if (pageSize > 0 && page > 0)
                data = data.Skip((page - 1) * pageSize);

            if (pageSize > 0)
                data = data.Take(pageSize);

            foreach (Expression<Func<TDb, object>> includeProperty in includeProperties)
            {
                data = data.Include<TDb, object>(includeProperty);
            }

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data);
            return result.AsQueryable();
        }
        public IQueryable<TApp> GetPagedInclude(Expression<Func<TApp, bool>> match, int page, int pageSize, params Expression<Func<TDb, object>>[] includeProperties)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));
            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            if (match != null)
            {
                var lambda = ConvertExpressionToDbType(match);
                data = data.Where(lambda);
            }

            if (pageSize > 0 && page > 0)
                data = data.Skip((page - 1) * pageSize);

            if (pageSize > 0)
                data = data.Take(pageSize);

            foreach (Expression<Func<TDb, object>> includeProperty in includeProperties)
            {
                data = data.Include<TDb, object>(includeProperty);
            }
            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data);
            return result.AsQueryable();
        }
        public IQueryable<TApp> GetPaged(Expression<Func<TApp, bool>> match, int page, int pageSize)
        {
            ContextToApp.MapperBase mapper = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(TDb));
            IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();

            if (match != null)
            {
                var lambda = ConvertExpressionToDbType(match);
                data = data.Where(lambda);
            }

            if (pageSize > 0 && page > 0)
                data = data.Skip((page - 1) * pageSize);

            if (pageSize > 0)
                data = data.Take(pageSize);

            var result = mapper.GetEntitiesFromDbObjects<TDb, TApp>(data);
            return result.AsQueryable();
        }
        //public IQueryable<TDb> GetAllIncluding(params Expression<Func<TDb, object>>[] includeProperties)
        //{
        //    var queryable = _context.Set<TDb>().AsNoTracking().AsQueryable();

        //    foreach (Expression<Func<TDb, object>> includeProperty in includeProperties)
        //    {
        //        queryable = queryable.Include<TDb, object>(includeProperty);
        //    }

        //    return queryable;
        //}
        private IQueryable<TDb> GetAllIncluding(IQueryable<TDb> queryable, params Expression<Func<TDb, object>>[] includeProperties)
        {
            foreach (Expression<Func<TDb, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TDb, object>(includeProperty);
            }

            return queryable;
        }

        //protected QueryResult<TOut> GetPaged<TIn, TOut>(IQueryable<TIn> queryable, DataSourceLoadOptionsMio dataSourceLoadOptions)
        //    where TIn : class
        //    where TOut : class
        //{
        //    dataSourceLoadOptions.ObjectType = typeof(TOut);
        //    var dataSourceLoadOptionsMapper = AppToContext.FactorySwitcher.GetFactoryFor(typeof(DataSourceLoadOptionsMio));
        //    var optionsMapped = dataSourceLoadOptionsMapper.GetDbObjectFromEntity<DataSourceLoadOptionsMio, DataSourceLoadOptionsBase>(dataSourceLoadOptions);
        //    var result = DataSourceLoader.Load(queryable, optionsMapped);
        //    var loadResultMapper = new LoadResultMapper<TIn, TOut>();
        //    var resultMapped = loadResultMapper.GetEntityFromDbObject(result);
        //    return resultMapped;
        //}

        //protected QueryResult<TApp> GetPaged(Expression<Func<TDb, bool>> match, Func<TDb, object> orderBy, DataSourceLoadOptionsMio dataSourceLoadOptions, params Expression<Func<TDb, object>>[] includeProperties)
        //{
        //    IQueryable<TDb> data = _context.Set<TDb>().AsNoTracking();
        //    if (includeProperties != null && includeProperties.Any())
        //    {
        //        data = GetAllIncluding(data, includeProperties);
        //    }
        //    if (match != null)
        //    {
        //        data = data.Where(match);
        //    }
        //    return GetPaged<TDb, TApp>(data, dataSourceLoadOptions);
        //}

        //public QueryResult<TApp> GetPaged(Expression<Func<TDb, bool>> match, DataSourceLoadOptionsMio dataSourceLoadOptions, params Expression<Func<TDb, object>>[] includeProperties)
        //{
        //    return GetPaged(match, _defaultOrderBy, dataSourceLoadOptions, includeProperties);
        //}

        //public QueryResult<TApp> GetPaged(DataSourceLoadOptionsMio dataSourceLoadOptions)
        //{
        //    return GetPaged(null, _defaultOrderBy, dataSourceLoadOptions, _defaultIncludeProperties);
        //}


    }
}
