using ProyectoWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
