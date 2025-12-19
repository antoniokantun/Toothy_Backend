using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Odontologos;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OdontologosController : ControllerBase
    {
        private readonly IOdontologoService _odontologoService;

        public OdontologosController(IOdontologoService odontologoService)
        {
            _odontologoService = odontologoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosOdontologo()
        {
            var odontologo = await _odontologoService.ObtenerTodosOdontologoAsync();
            return Ok(odontologo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOdontologoPorId(int id)
        {
            var odontologo = await _odontologoService.ObtenerOdontologoPorIdAsync(id);
            if (odontologo == null)
                return NotFound($"No existe el odontologo con ID {id}");
            return Ok(odontologo);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarOdontologo([FromBody] CreateOdontologoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _odontologoService.RegistrarOdontologoAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOdontologo(int id, [FromBody] UpdateOdontologoDto dto)
        {
            try
            {
                await _odontologoService.ActualizarOdontologoAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarOdontologo(int id)
        {
            try
            {
                await _odontologoService.EliminarOdontologoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
