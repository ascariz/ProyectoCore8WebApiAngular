using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Data.Models
{
    public class ProyectoImg
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Titulo { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Imagen { get; set; }

        [Required]
        public int Orden { get; set; }

        public int ProyectoId { get; set; }
        public virtual Proyecto Proyecto { get; set; }

    }
}
