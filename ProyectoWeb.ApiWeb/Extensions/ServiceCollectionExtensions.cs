using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProyectoWeb.Data;
using ProyectoWeb.Interface.Aplicacion;
using ProyectoWeb.Interface.Dominio;
using ProyectoWeb.Repository;
using ProyectoWeb.Service;
using System.Globalization;

namespace ProyectoWeb.ApiWeb.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            return services;
        }

        public static IServiceCollection ConfigureServicioRepository(this IServiceCollection services, WebApplicationBuilder builder)
        {

            #region Proyectos

            services.AddScoped<IProyectoService, ProyectoService>();
            services.AddScoped<IProyectoRepository, ProyectoRepository>();


            #endregion

            return services;
        }
    }
}
