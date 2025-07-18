using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion.Base;

namespace ProyectoWeb.Interface.Aplicacion
{
    public interface IApplicationUserService : IBaseService<ApplicationUserDto>
    {
        /// <summary>
        /// Validaciones de la entidad ApplicationUser
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Validacion(ApplicationUserDto dto);

        /// <summary>
        /// Duplicado de la entidad ApplicationUser
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ApplicationUserDto EsDuplicado(string texto);
    }
}
