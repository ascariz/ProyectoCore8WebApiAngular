using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProyectoWeb.Data;
using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Repository.Base;

namespace ProyectoWeb.Repository
{
    public class ProyectoRepository : BaseRepository<ProyectoDto, Proyecto>, IProyectoRepository
    {
        private ILogger<ProyectoRepository> _log;

        public ProyectoRepository(IMemoryCache memoryCache, ApplicationDbContext context, ILogger<ProyectoRepository> log)
            : base(context, memoryCache)
        {
            _log = log;
        }
    }
}
