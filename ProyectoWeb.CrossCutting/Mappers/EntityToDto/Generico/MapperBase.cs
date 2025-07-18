namespace ProyectoWeb.CrossCutting.Mappers.EntityToDto
{
    public abstract class MapperBase
    {
        public virtual IEnumerable<TOutput> GetEntitiesFromDbObjects<TInput, TOutput>(IEnumerable<TInput> dbobjects)
          where TInput : class
          where TOutput : class
        {
            if (dbobjects == null)
            {
                yield break;
            }

            foreach (var item in dbobjects)
            {
                yield return GetEntityFromDbObject<TInput, TOutput>(item);
            }
        }

        public abstract TOutput GetEntityFromDbObject<TInput, TOutput>(TInput dbobject)
          where TInput : class
          where TOutput : class;


    }
}


