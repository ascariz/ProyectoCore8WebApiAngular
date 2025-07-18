using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;

namespace ProyectoWeb.CrossCutting.Mappers.DtoToEntity
{
    public class ApplicationUserDtoToEntityMapper : MapperBase
    {
        public override TOutput GetDbObjectFromEntity<TInput, TOutput>(TInput appobject)
        {
            if (appobject == null)
                return default(TOutput);

            var appEntity = appobject as ApplicationUserDto;
            if (appEntity == null)
                throw new InvalidCastException("Cast to type ApplicationUserDto has failed.");

            var dbEntity = new ApplicationUser()
            {
               Nombre = appEntity.Nombre,Apellidos = appEntity.Apellidos,Nick = appEntity.Nick,NumeroLogin = appEntity.NumeroLogin,Id = appEntity.Id,UserName = appEntity.UserName,NormalizedUserName = appEntity.NormalizedUserName,Email = appEntity.Email,NormalizedEmail = appEntity.NormalizedEmail,EmailConfirmed = appEntity.EmailConfirmed,PasswordHash = appEntity.PasswordHash,SecurityStamp = appEntity.SecurityStamp,ConcurrencyStamp = appEntity.ConcurrencyStamp,PhoneNumber = appEntity.PhoneNumber,PhoneNumberConfirmed = appEntity.PhoneNumberConfirmed,TwoFactorEnabled = appEntity.TwoFactorEnabled,LockoutEnd = appEntity.LockoutEnd,LockoutEnabled = appEntity.LockoutEnabled,AccessFailedCount = appEntity.AccessFailedCount,
            };
            return dbEntity as TOutput;
        }
    }
}