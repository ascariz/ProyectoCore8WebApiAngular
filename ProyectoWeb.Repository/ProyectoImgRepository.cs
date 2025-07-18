using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProyectoWeb.Data;
using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Repository.Base;

namespace ProyectoWeb.Repository
{
    public class ProyectoImgRepository : BaseRepository<ProyectoImgDto, ProyectoImg>, IProyectoImgRepository
    {
        private ILogger<ProyectoImgRepository> _log;

        public ProyectoImgRepository(IMemoryCache memoryCache, ApplicationDbContext context, ILogger<ProyectoImgRepository> log)
            : base(context, memoryCache)
        {
            _log = log;
        }
    }
}