using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Usuarios;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosUsuario()
        { 
            var usuarios = await _usuarioService.ObtenerTodosUsuarioAsync();
            return Ok(usuarios);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] CreateUsuarioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            { 
                var nuevoUsuario = await _usuarioService.RegistrarUsuarioAsync(dto);
                return Ok(nuevoUsuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error interno en el servidor: {ex.Message}");
            }
        }
    }
}
