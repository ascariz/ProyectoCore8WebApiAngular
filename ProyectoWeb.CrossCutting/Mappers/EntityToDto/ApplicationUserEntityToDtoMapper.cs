using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;

namespace ProyectoWeb.CrossCutting.Mappers.EntityToDto
{
    public class ApplicationUserEntityToDtoMapper : MapperBase
    {
        public override TOutput GetEntityFromDbObject<TInput, TOutput>(TInput dbobject)
        {
            if (dbobject == null)
                return default(TOutput);

            var dbEntity = dbobject as ApplicationUser;
            if (dbEntity == null)
                throw new InvalidCastException("Cast to type ApplicationUser has failed.");

            var appEntity = new ApplicationUserDto()
            {
                Nombre = dbEntity.Nombre,Apellidos = dbEntity.Apellidos,Nick = dbEntity.Nick,NumeroLogin = dbEntity.NumeroLogin,Id = dbEntity.Id,UserName = dbEntity.UserName,NormalizedUserName = dbEntity.NormalizedUserName,Email = dbEntity.Email,NormalizedEmail = dbEntity.NormalizedEmail,EmailConfirmed = dbEntity.EmailConfirmed,PasswordHash = dbEntity.PasswordHash,SecurityStamp = dbEntity.SecurityStamp,ConcurrencyStamp = dbEntity.ConcurrencyStamp,PhoneNumber = dbEntity.PhoneNumber,
                PhoneNumberConfirmed = dbEntity.PhoneNumberConfirmed,TwoFactorEnabled = dbEntity.TwoFactorEnabled,
                LockoutEnd = dbEntity.LockoutEnd,
                LockoutEnabled = dbEntity.LockoutEnabled,AccessFailedCount = dbEntity.AccessFailedCount,
            };

            //if (dbEntity.Idioma != null)
            //{
            //    ContextToApp.MapperBase mapperUser = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(SwCardProject.Data.Models.Idioma));
            //    appEntity.Idioma = mapperUser.GetEntityFromDbObject<SwCardProject.Data.Models.Idioma, IdiomaDto>(dbEntity.Idioma);
            //}
            //if (dbEntity.Pais != null)
            //{
            //    ContextToApp.MapperBase mapperUser = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(Pais));
            //    appEntity.Pais = mapperUser.GetEntityFromDbObject<Pais, PaisDto>(dbEntity.Pais);
            //}

            //if (dbEntity.Provincia != null)
            //{
            //    ContextToApp.MapperBase mapperUser = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(Provincia));
            //    appEntity.Provincia = mapperUser.GetEntityFromDbObject<Provincia, ProvinciaDto>(dbEntity.Provincia);
            //}

            //if (dbEntity.Localidad != null)
            //{
            //    ContextToApp.MapperBase mapperUser = ContextToApp.FactorySwitcher.GetFactoryFor(typeof(Localidad));
            //    appEntity.Localidad = mapperUser.GetEntityFromDbObject<Localidad, LocalidadDto>(dbEntity.Localidad);
            //}





            return appEntity as TOutput;
        }
    }
}
