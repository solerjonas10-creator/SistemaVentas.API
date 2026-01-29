using Microsoft.AspNetCore.Mvc;
using SistemaVentas.API.Data;
using SistemaVentas.API.Models;
using SistemaVentas.API.Validators;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly VentasContext _context;

        public ClienteController(VentasContext context)
        {
            _context = context;
        }

        [HttpGet("Read/{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var cliente = await _context.CLIENTES.FindAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validator = new ClienteValidator();
            var resultado = validator.Validate(cliente);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            _context.CLIENTES.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Read), new { id = cliente.Id }, cliente);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente clientemod)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != clientemod.Id) return BadRequest("id y cliente.id son diferentes.");

            var cliente = await _context.CLIENTES.FindAsync(id);
            if (cliente == null) return NotFound();

            var validator = new ClienteValidator();
            var resultado = validator.Validate(clientemod);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            cliente.Nombres = clientemod.Nombres;
            cliente.Apellidos = clientemod.Apellidos;
            cliente.NroDoc = clientemod.NroDoc;
            cliente.Correo = clientemod.Correo;
            cliente.Telefono = clientemod.Telefono;
            cliente.Direccion = clientemod.Direccion;
            cliente.Activo = clientemod.Activo;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Cliente actualizado.", cliente });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.CLIENTES.FindAsync(id);
            if (cliente == null) return NotFound();

            _context.CLIENTES.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cliente eliminado." });
        }
    }
}
