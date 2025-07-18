using Microsoft.AspNetCore.Mvc;
using ProyectoWeb.DTO;
using ProyectoWeb.Interface.Aplicacion;

namespace ProyectoWeb.ApiWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProyectoController : ControllerBase
    {
        
        private readonly ILogger<ProyectoController> _logger;
        private readonly IProyectoService _proyectoService;

        public ProyectoController(ILogger<ProyectoController> logger,IProyectoService proyectoService)
        {
            _logger = logger;
            _proyectoService = proyectoService;
        }

        //[HttpGet(Name = "GetAll")]
        //public IEnumerable<ProyectoDto> GetAll()
        //{
        //   _logger.LogInformation("GetAll method called in ProyectoController");
        //    try
        //    {
        //        var proyectos = _proyectoService.GetAll();
        //        return proyectos;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while fetching all projects.");
        //        throw; // Consider returning a more user-friendly error response
        //    }   
        //}
        [HttpGet]
        public IActionResult GetAll()
        {
            var proyectos = _proyectoService.GetAll();
            return Ok(proyectos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var proyecto = _proyectoService.Get(id);
            return proyecto == null ? NotFound() : Ok(proyecto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProyectoDto dto)
        {
            var creado = _proyectoService.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProyectoDto dto)
        {
            var actualizado = _proyectoService.Put(dto, dto.Id);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
           _proyectoService.Delete(id);
            return NoContent();

        }
    }
}
