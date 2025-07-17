using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWeb.DTO
{
    public class ProyectoDto
    {
        public int Id { get; set; }
      
        public string Empresa { get; set; }
       
        public string URL { get; set; }
      
        public string Web { get; set; }
        [Required]
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
