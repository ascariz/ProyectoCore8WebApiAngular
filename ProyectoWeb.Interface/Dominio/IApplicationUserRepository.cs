using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Dominio.Base;

namespace ProyectoWeb.Interface.Dominio
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUserDto, ApplicationUser>
    {
    }
}
