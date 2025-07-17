using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProyectoWeb.DTO
{
    public class ApplicationUserDto : IdentityUser
    {
        public string Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string Nick { get; set; }

        public int NumeroLogin { get; set; }
    }
}
