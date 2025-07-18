using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion.Base;

namespace ProyectoWeb.Interface.Aplicacion
{
    public interface IProyectoImgService : IBaseService<ProyectoImgDto>
    {
        /// <summary>
        /// Validaciones de la entidad ProyectoImg
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Validacion(ProyectoImgDto dto);

        /// <summary>
        /// Duplicado de la entidad ProyectoImg
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ProyectoImgDto EsDuplicado(string texto);
    }
}
