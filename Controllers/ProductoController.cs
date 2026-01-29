using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            return Ok(new {id, Nombre = "Jonas"});
        }
    }
}
