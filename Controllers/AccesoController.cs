using FluentValidation;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.API.Custom;
using SistemaVentas.API.Data;
using SistemaVentas.API.Models;
using SistemaVentas.API.Models.DTOs;
using SistemaVentas.API.Validators;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly VentasContext _context;
        private readonly Utils _utils;
        public AccesoController(VentasContext context, Utils utils  )
        {
            _context = context;
            _utils = utils;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO usuarioDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var validator = new UsuarioValidator();

            var modelUsuario = new Usuario
            {
                NombreUsuario = usuarioDTO.NombreUsuario,
                Correo = usuarioDTO.Correo,
                Clave = _utils.encriptarSHA256(usuarioDTO.Clave),
                Rol = "USER",
                NroDoc = usuarioDTO.NroDoc,
                Telefono = usuarioDTO.Telefono,
                Direccion = usuarioDTO.Direccion,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                Activo = 1,
                Registrado = DateTime.Now
            };

            var resultado = validator.Validate(modelUsuario);

            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var usuarioExistente = await _context.USUARIOS
                .Where(u => u.Correo == usuarioDTO.Correo || u.NombreUsuario == usuarioDTO.NombreUsuario)
                .FirstOrDefaultAsync();

            if (usuarioExistente != null)
            {
                return BadRequest(new { message = "El correo o nombre de usuario ya está en uso." });
            }

            await _context.USUARIOS.AddAsync(modelUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado correctamente." , modelUsuario});
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var validator = new LoginValidator();
            var resultado = validator.Validate(loginDTO);
            if (resultado.IsValid == false)
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var usuarioEncontrado = await _context.USUARIOS
                .Where(u =>
                    u.Correo == loginDTO.Correo &&
                    u.Clave == _utils.encriptarSHA256(loginDTO.Clave) &&
                    u.Activo == 1
                ).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                return BadRequest(new { message = "Credenciales incorrectas o usuario inactivo." });
            }
            else
            {
                return Ok(new { message = "Login exitoso.", token = _utils.generarJWT(usuarioEncontrado) });
            }
        }
    }
}
