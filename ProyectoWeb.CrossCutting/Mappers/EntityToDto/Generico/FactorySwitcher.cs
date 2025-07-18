namespace ProyectoWeb.CrossCutting.Mappers.EntityToDto
{
    public class FactorySwitcher
    {
        public static MapperBase GetFactoryFor(Type typeOfdbObject)
        {
            switch (typeOfdbObject.Name)
            {
                 case "ApplicationUser": return new ApplicationUserEntityToDtoMapper();  case "Proyecto": return new ProyectoEntityToDtoMapper();  case "ProyectoImg": return new ProyectoImgEntityToDtoMapper(); 


                default:
                    throw new NotImplementedException($"The factory for type {typeOfdbObject.Name} is not implemented.");
            }
        }
    }
}
