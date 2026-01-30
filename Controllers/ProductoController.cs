using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.API.Data;
using SistemaVentas.API.Models;
using System.ComponentModel.DataAnnotations;
using SistemaVentas.API.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly VentasContext _context;

        public ProductoController(VentasContext context)
        {
            _context = context;
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Read(int id)
        {
            var producto = await _context.PRODUCTOS.FindAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validator = new ProductoValidator();
            var resultado = validator.Validate(producto);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            _context.PRODUCTOS.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Read), new { id = producto.Id }, producto);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto productomod)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != productomod.Id) return BadRequest("id y producto.id son diferentes.");

            var producto = await _context.PRODUCTOS.FindAsync(id);
            if (producto == null) return NotFound();

            var validator = new ProductoValidator();
            var resultado = validator.Validate(productomod);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            producto.Descripcion = productomod.Descripcion;
            producto.PrecioCompra = productomod.PrecioCompra;
            producto.PrecioVenta = productomod.PrecioVenta;
            producto.Iva = productomod.Iva;
            producto.Stock = productomod.Stock;
            producto.Activo = productomod.Activo;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Producto actualizado.", producto });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.PRODUCTOS.FindAsync(id);
            if (producto == null) return NotFound();

            _context.PRODUCTOS.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Producto eliminado." });
        }
    }
}
