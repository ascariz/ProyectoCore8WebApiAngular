using Microsoft.Extensions.Caching.Memory;
using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Service.Base;

namespace ProyectoWeb.Service
{
    public class ProyectoService :  BaseService<ProyectoDto, Proyecto, IProyectoRepository>, IProyectoService
    {
        private IProyectoRepository _repo;
        private readonly IMemoryCache _memoryCache;

        public ProyectoService(IProyectoRepository repo, IMemoryCache memoryCache) : base(repo, memoryCache)
        {
            _repo = repo;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// validadcion de horas si es correcto true
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Validacion(ProyectoDto dto)
        {
            // ejemplo
            // if (dto.LuJuHoraEntrada < dto.LuJuHoraSalida)
            //     return false;

            return true;
        }
        /// <summary>
        /// duplicado
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public ProyectoDto EsDuplicado(string texto)
        {
            //var dto = _repo.Find(x => x.Descripcion == texto);
            //if (dto != null)
            //{               
            //    return dto;
            //}
            return new ProyectoDto();
        }
    }
}
