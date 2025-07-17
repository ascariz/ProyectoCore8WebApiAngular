using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Interface.Dominio
{
    public interface IProyectoRepository : IBaseRepository<ProyectoDto, Proyecto>
    {
    }
}
