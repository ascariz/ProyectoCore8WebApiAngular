using ProyectoWeb.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        #region Genericos
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        #endregion

        #region WebAscariz

        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<ProyectoImg> ProyectoImg { get; set; }


        #endregion



    }
}
