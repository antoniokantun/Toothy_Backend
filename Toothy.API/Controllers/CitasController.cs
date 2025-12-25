using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Citas;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;
        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpPost]
        public async Task<IActionResult> AgendarCita([FromBody] CreateCitaDto dto)
        {
            try
            {
                var citaCreada = await _citaService.RegistrarCitaAsync(dto);
                return Ok(citaCreada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
