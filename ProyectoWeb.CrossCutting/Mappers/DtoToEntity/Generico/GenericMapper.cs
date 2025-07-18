namespace ProyectoWeb.CrossCutting.Mappers.DtoToEntity
{
   public class GenericMapper
         <TInput, TOutput>
             where TInput : class
             where TOutput : class

    {
        public TOutput Create(TInput entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var factory = FactorySwitcher.GetFactoryFor(typeof(TInput));
            var mapped = factory.GetDbObjectFromEntity<TInput, TOutput>(entity);
            return mapped;
        }

        public IEnumerable<TOutput> Create(IEnumerable<TInput> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var factory = FactorySwitcher.GetFactoryFor(typeof(TInput));
            var mapped = factory.GetDbObjectsFromEntities<TInput, TOutput>(entity);
            return mapped;
        }
    }
}
