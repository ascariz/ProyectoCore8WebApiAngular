using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProyectoWeb.Data;
using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Repository.Base;
//using DtoToEntry = ProyectoWeb.CrossCutting.Mappers.DtoToEntry;
//using EntryToDto = ProyectoWeb.CrossCutting.Mappers.EntryToDto;

namespace ProyectoWeb.Repository
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUserDto, ApplicationUser>, IApplicationUserRepository
    {
        private ILogger<ApplicationUserRepository> _log;

        public ApplicationUserRepository(IMemoryCache memoryCache, ApplicationDbContext context, ILogger<ApplicationUserRepository> log)
            : base(context, memoryCache)
        {
            _log = log;
        }
    }
}