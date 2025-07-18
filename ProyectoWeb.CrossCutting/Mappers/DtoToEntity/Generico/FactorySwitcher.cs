namespace ProyectoWeb.CrossCutting.Mappers.DtoToEntity
{
    public class FactorySwitcher
    {
        public static MapperBase GetFactoryFor(Type typeOfdbObject)
        {
            switch (typeOfdbObject.Name)
            {
                 case "ApplicationUserDto": return new ApplicationUserDtoToEntityMapper();  case "ProyectoDto": return new ProyectoDtoToEntityMapper();  case "ProyectoImgDto": return new ProyectoImgDtoToEntityMapper(); 


                default:
                    throw new NotImplementedException($"The factory for type {typeOfdbObject.Name} is not implemented.");
            }
        }
    }
}
