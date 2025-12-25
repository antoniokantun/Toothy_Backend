using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Recepcionistas;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecepcionistasController : ControllerBase
    {
        private readonly IRecepcionistaService _recepcionistaService;

        public RecepcionistasController(IRecepcionistaService recepcionistaService)
        {
            _recepcionistaService = recepcionistaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosRecepcionista()
        {
            return Ok(await _recepcionistaService.ObtenerTodosRecepcionistaAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRecepcionistaPorId(int id)
        {
            var recepcionista = await _recepcionistaService.ObtenerRecepcionistaPorIdAsync(id);
            if (recepcionista == null)
                return NotFound($"No existe recepcionista con ID {id}");
            return Ok(recepcionista);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRecepcionista([FromBody] CreateRecepcionistaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
               var nuevoRecepcionista = await _recepcionistaService.CrearRecepcionistaAsync(dto);
               return Ok(nuevoRecepcionista);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el recepcionista: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRecepcionista(int id, [FromBody] UpdateRecepcionistaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _recepcionistaService.ActualizarRecepcionistaAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRecepcionista(int id)
        {
            try
            {
                await _recepcionistaService.EliminarRecepcionistaAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
