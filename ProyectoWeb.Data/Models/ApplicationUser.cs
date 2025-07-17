using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWeb.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "NVARCHAR(20)")]
        public string Nombre { get; set; }

        [PersonalData]
        [Column(TypeName = "NVARCHAR(50)")]
        public string? Apellidos { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "NVARCHAR(70)")]
        public string Nick { get; set; }

        [PersonalData]
        public int NumeroLogin { get; set; }
    }
}
