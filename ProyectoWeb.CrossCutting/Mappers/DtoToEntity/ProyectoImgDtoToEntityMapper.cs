using ProyectoWeb.Data.Models;
using ProyectoWeb.DTO;

namespace ProyectoWeb.CrossCutting.Mappers.DtoToEntity
{
    public class ProyectoImgDtoToEntityMapper : MapperBase
    {
        public override TOutput GetDbObjectFromEntity<TInput, TOutput>(TInput appobject)
        {
            if (appobject == null)
                return default(TOutput);

            var appEntity = appobject as ProyectoImgDto;
            if (appEntity == null)
                throw new InvalidCastException("Cast to type ProyectoImgDto has failed.");

            var dbEntity = new ProyectoImg()
            {
               Id = appEntity.Id,Titulo = appEntity.Titulo,Imagen = appEntity.Imagen,Orden = appEntity.Orden,ProyectoId = appEntity.ProyectoId,
            };
            return dbEntity as TOutput;
        }
    }
}