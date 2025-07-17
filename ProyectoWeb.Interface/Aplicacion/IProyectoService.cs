using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion.Base;

namespace ProyectoWeb.Interface.Aplicacion
{
    public interface IProyectoService : IBaseService<ProyectoDto>
    {
        /// <summary>
        /// Validaciones de la entidad
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Validacion(ProyectoDto dto);

        /// <summary>
        /// Duplicado de la entidad
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ProyectoDto EsDuplicado(string texto);
    }
}
