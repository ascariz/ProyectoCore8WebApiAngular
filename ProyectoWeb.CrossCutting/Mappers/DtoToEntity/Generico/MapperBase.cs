namespace ProyectoWeb.CrossCutting.Mappers.DtoToEntity
{
    public abstract class MapperBase
    {
        public virtual IEnumerable<TOutput> GetDbObjectsFromEntities<TInput, TOutput>(IEnumerable<TInput> dbobjects)
          where TInput : class
          where TOutput : class
        {
            if (dbobjects == null)
            {
                yield break;
            }

            foreach (var item in dbobjects)
            {
                yield return GetDbObjectFromEntity<TInput, TOutput>(item);
            }
        }

        public abstract TOutput GetDbObjectFromEntity<TInput, TOutput>(TInput dbobject)
          where TInput : class
          where TOutput : class;

        public virtual string MapMemberFromApplicationToEntity(string modelMember)
        {
            switch (modelMember)
            {
                default:
                    return modelMember;
            }
        }
    }
}
