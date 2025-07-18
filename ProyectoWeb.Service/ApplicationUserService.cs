using Microsoft.Extensions.Caching.Memory;
using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Service.Base;

namespace ProyectoWeb.Service
{
    public class ApplicationUserService :  BaseService<ApplicationUserDto, ApplicationUser, IApplicationUserRepository>, IApplicationUserService
    {
        private IApplicationUserRepository _repo;
        private readonly IMemoryCache _memoryCache;

        public ApplicationUserService(IApplicationUserRepository repo, IMemoryCache memoryCache) : base(repo, memoryCache)
        {
            _repo = repo;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// validadcion  ApplicationUser
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Validacion(ApplicationUserDto dto)
        {
            // ejemplo
            // if (dto.LuJuHoraEntrada < dto.LuJuHoraSalida)
            //     return false;

            return true;
        }
        /// <summary>
        /// duplicado ApplicationUser
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public ApplicationUserDto EsDuplicado(string texto)
        {
            //var dto = _repo.Find(x => x.Descripcion == texto);
            //if (dto != null)
            //{               
            //    return dto;
            //}
            return new ApplicationUserDto();
        }
    }
}
