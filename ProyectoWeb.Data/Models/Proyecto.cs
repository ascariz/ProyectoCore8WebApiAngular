using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Data.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Empresa { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string URL { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Web { get; set; }
        [Required]
        public bool WebActivo { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public bool Portada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string MiniDescripcion { get; set; }


        [Column(TypeName = "nvarchar(2500)")]
        public string Descripcion { get; set; }


        [Column(TypeName = "nvarchar(150)")]
        public string Titulo { get; set; }

       
        [Column(TypeName = "nvarchar(255)")]
        public string MetaTexto { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string MetaPalabra { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Imagen { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Programacion { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Tecnologias { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Secciones { get; set; }


        
    }
}
