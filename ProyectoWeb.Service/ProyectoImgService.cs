using Microsoft.Extensions.Caching.Memory;
using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Service.Base;

namespace ProyectoWeb.Service
{
    public class ProyectoImgService :  BaseService<ProyectoImgDto, ProyectoImg, IProyectoImgRepository>, IProyectoImgService
    {
        private IProyectoImgRepository _repo;
        private readonly IMemoryCache _memoryCache;

        public ProyectoImgService(IProyectoImgRepository repo, IMemoryCache memoryCache) : base(repo, memoryCache)
        {
            _repo = repo;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// validadcion  ProyectoImg
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Validacion(ProyectoImgDto dto)
        {
            // ejemplo
            // if (dto.LuJuHoraEntrada < dto.LuJuHoraSalida)
            //     return false;

            return true;
        }
        /// <summary>
        /// duplicado ProyectoImg
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public ProyectoImgDto EsDuplicado(string texto)
        {
            //var dto = _repo.Find(x => x.Descripcion == texto);
            //if (dto != null)
            //{               
            //    return dto;
            //}
            return new ProyectoImgDto();
        }
    }
}
