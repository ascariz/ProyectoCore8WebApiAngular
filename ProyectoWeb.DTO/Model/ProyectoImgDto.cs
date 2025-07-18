namespace ProyectoWeb.DTO
{
    public class ProyectoImgDto
    {

        public ProyectoImgDto()
        {
           
        }      

        public int Id { get;set; }
public string Titulo { get;set; }
public string Imagen { get;set; }
public int Orden { get;set; }
public int ProyectoId { get;set; }
public  virtual ProyectoImgDto Proyecto {get;set;}

    }
}
