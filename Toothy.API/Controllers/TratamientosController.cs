using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Tratamientos;
using Toothy.Application.Services.Implementations;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TratamientosController : ControllerBase
    {
        private readonly ITratamientoService _tratamientoService;
        public TratamientosController(TratamientoService tratamientoService)
        {
            _tratamientoService = tratamientoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosTratamientoAsync()
        {
            try
            {
                var tratamientos = await _tratamientoService.ObtenerTodosTratamientoAsync();
                return Ok(tratamientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearTratamientoAsync([FromBody] CreateTratamientoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = await _tratamientoService.CrearTratamientoAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}