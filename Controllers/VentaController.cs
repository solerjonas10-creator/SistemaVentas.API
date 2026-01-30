using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.API.Data;
using SistemaVentas.API.Models;
using SistemaVentas.API.Validators;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentasContext _context;

        public VentaController(VentasContext context)
        {
            _context = context;
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Read(int id)
        {
            var venta = await _context.VENTAS
                .Include(v => v.DETALLE_VENTAS)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
            {
                return NotFound(new { message = $"No se encontró la venta con ID {id}" });
            }

            return Ok(venta);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Venta venta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var validator = new VentaValidator();
            var resultado = validator.Validate(venta);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            Cliente? cliente = await _context.CLIENTES.FindAsync(venta.IdCliente);
            if (cliente == null)
            {
                ModelState.AddModelError("IdCliente", $"El producto con Id {venta.IdCliente} no existe.");
                return BadRequest(ModelState);
            }

            var validator2 = new DetalleVentaValidator();
            Producto? producto;

            // valida datos de los detalles
            foreach(var detalle in venta.DETALLE_VENTAS)
            {
                resultado = validator2.Validate(detalle);

                if (resultado.IsValid == false)
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                producto = await _context.PRODUCTOS.FindAsync(detalle.IdProducto);

                // si existe producto valida y actualiza stock, y asigna precio
                if(producto == null)
                {
                    ModelState.AddModelError("IdProducto", $"El producto con Id {detalle.IdProducto} no existe.");
                    return BadRequest(ModelState);
                }
                else
                {
                    detalle.Precio = (decimal)producto.PrecioVenta!;

                    if (producto.Stock < detalle.Cantidad)
                    {
                        ModelState.AddModelError("Cantidad", $"El producto {producto.Descripcion} no tiene stock suficiente. Stock actual: {producto.Stock}.");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        producto.Stock -= detalle.Cantidad;
                    }
                }
            }

            _context.VENTAS.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Read),
                new { id = venta.Id },
                new
                {
                    venta.Id,
                    venta.Fecha,
                    venta.IdCliente
                }
            );

        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Venta ventamod)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != ventamod.Id) return BadRequest("id y venta.id son diferentes.");

            var venta = await _context.VENTAS.FindAsync(id);
            if (venta == null) return NotFound("Venta no encontrada.");

            var detalles = _context.DETALLE_VENTAS.Where(d => d.IdVenta == id);

            foreach (var detalle in detalles)
            {
                _context.PRODUCTOS.Where(p => p.Id == detalle.IdProducto)
                    .ToList()
                    .ForEach(p => p.Stock += detalle.Cantidad);
            }

            var validator = new VentaValidator();
            var resultado = validator.Validate(ventamod);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            Cliente? cliente = await _context.CLIENTES.FindAsync(ventamod.IdCliente);
            if (cliente == null)
            {
                ModelState.AddModelError("IdCliente", $"El producto con Id {venta.IdCliente} no existe.");
                return BadRequest(ModelState);
            }

            var validator2 = new DetalleVentaValidator();
            Producto? producto;

            // valida datos de los detalles
            foreach (var detalle in ventamod.DETALLE_VENTAS)
            {
                detalle.IdVenta = id;
                resultado = validator2.Validate(detalle);


                if (resultado.IsValid == false)
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                producto = await _context.PRODUCTOS.FindAsync(detalle.IdProducto);

                // si existe producto valida y actualiza stock, y asigna precio
                if (producto == null)
                {
                    ModelState.AddModelError("IdProducto", $"El producto con Id {detalle.IdProducto} no existe.");
                    return BadRequest(ModelState);
                }
                else
                {
                    detalle.Precio = (decimal)producto.PrecioVenta!;

                    if (producto.Stock < detalle.Cantidad)
                    {
                        ModelState.AddModelError("Cantidad", $"El producto {producto.Descripcion} no tiene stock suficiente. Stock actual: {producto.Stock}.");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        producto.Stock -= detalle.Cantidad;
                    }
                }
            }

            _context.DETALLE_VENTAS.RemoveRange(detalles);
            _context.Entry(venta).CurrentValues.SetValues(ventamod);
            _context.DETALLE_VENTAS.AddRange(ventamod.DETALLE_VENTAS);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Venta actualizada.", Id = venta.Id, Fecha = venta.Fecha, IdCliente = venta.IdCliente});
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var venta = await _context.VENTAS.FindAsync(id);
            if (venta == null) return NotFound();

            var detalles = _context.DETALLE_VENTAS.Where(d => d.IdVenta == id);

            foreach(var detalle in detalles)
            {
                _context.PRODUCTOS.Where(p => p.Id == detalle.IdProducto)
                    .ToList()
                    .ForEach(p => p.Stock += detalle.Cantidad);
            }

            _context.DETALLE_VENTAS.RemoveRange(detalles);
            _context.VENTAS.Remove(venta);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Venta eliminada." });
        }
    }
}
