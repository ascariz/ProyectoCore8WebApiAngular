using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;

namespace ProyectoWeb.CrossCutting.Mappers.EntityToDto
{
    public class ProyectoEntityToDtoMapper : MapperBase
    {
        public override TOutput GetEntityFromDbObject<TInput, TOutput>(TInput dbobject)
        {
            if (dbobject == null)
                return default(TOutput);

            var dbEntity = dbobject as Proyecto;
            if (dbEntity == null)
                throw new InvalidCastException("Cast to type Proyecto has failed.");

            var appEntity = new ProyectoDto()
            {
                Id = dbEntity.Id,Empresa = dbEntity.Empresa,URL = dbEntity.URL,Web = dbEntity.Web,WebActivo = dbEntity.WebActivo,Activo = dbEntity.Activo,Portada = dbEntity.Portada,FechaCreacion = dbEntity.FechaCreacion,FechaModificacion = dbEntity.FechaModificacion,MiniDescripcion = dbEntity.MiniDescripcion,Descripcion = dbEntity.Descripcion,Titulo = dbEntity.Titulo,MetaTexto = dbEntity.MetaTexto,MetaPalabra = dbEntity.MetaPalabra,Imagen = dbEntity.Imagen,Programacion = dbEntity.Programacion,Tecnologias = dbEntity.Tecnologias,Secciones = dbEntity.Secciones,
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
