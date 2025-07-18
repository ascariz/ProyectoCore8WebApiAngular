using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;

namespace ProyectoWeb.CrossCutting.Mappers.DtoToEntity
{
    public class ProyectoDtoToEntityMapper : MapperBase
    {
        public override TOutput GetDbObjectFromEntity<TInput, TOutput>(TInput appobject)
        {
            if (appobject == null)
                return default(TOutput);

            var appEntity = appobject as ProyectoDto;
            if (appEntity == null)
                throw new InvalidCastException("Cast to type ProyectoDto has failed.");

            var dbEntity = new Proyecto()
            {
               Id = appEntity.Id,Empresa = appEntity.Empresa,URL = appEntity.URL,Web = appEntity.Web,WebActivo = appEntity.WebActivo,Activo = appEntity.Activo,Portada = appEntity.Portada,FechaCreacion = appEntity.FechaCreacion,FechaModificacion = appEntity.FechaModificacion,MiniDescripcion = appEntity.MiniDescripcion,Descripcion = appEntity.Descripcion,Titulo = appEntity.Titulo,MetaTexto = appEntity.MetaTexto,MetaPalabra = appEntity.MetaPalabra,Imagen = appEntity.Imagen,Programacion = appEntity.Programacion,Tecnologias = appEntity.Tecnologias,Secciones = appEntity.Secciones,
            };
            return dbEntity as TOutput;
        }
    }
}