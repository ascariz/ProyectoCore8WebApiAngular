using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;

namespace ProyectoWeb.CrossCutting.Mappers.EntityToDto
{
    public class ProyectoImgEntityToDtoMapper : MapperBase
    {
        public override TOutput GetEntityFromDbObject<TInput, TOutput>(TInput dbobject)
        {
            if (dbobject == null)
                return default(TOutput);

            var dbEntity = dbobject as ProyectoImg;
            if (dbEntity == null)
                throw new InvalidCastException("Cast to type ProyectoImg has failed.");

            var appEntity = new ProyectoImgDto()
            {
                Id = dbEntity.Id,Titulo = dbEntity.Titulo,Imagen = dbEntity.Imagen,Orden = dbEntity.Orden,ProyectoId = dbEntity.ProyectoId,
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
