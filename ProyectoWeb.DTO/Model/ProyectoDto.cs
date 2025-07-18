namespace ProyectoWeb.DTO
{
    public class ProyectoDto
    {

        public ProyectoDto()
        {
           
        }      

        public int Id { get;set; }
public string Empresa { get;set; }
public string URL { get;set; }
public string Web { get;set; }
public bool WebActivo { get;set; }
public bool Activo { get;set; }
public bool Portada { get;set; }
public DateTime FechaCreacion { get;set; }
public DateTime FechaModificacion { get;set; }
public string MiniDescripcion { get;set; }
public string Descripcion { get;set; }
public string Titulo { get;set; }
public string MetaTexto { get;set; }
public string MetaPalabra { get;set; }
public string Imagen { get;set; }
public string Programacion { get;set; }
public string Tecnologias { get;set; }
public string Secciones { get;set; }

    }
}
