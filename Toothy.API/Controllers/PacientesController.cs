using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Pacientes;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;
        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPaciente()
        {
            var pacientes = await _pacienteService.ObtenerTodosPacienteAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPacientePorId(int id)
        {
            var paciente = await _pacienteService.ObtenerPacientePorIdAsync(id);
            if (paciente == null)
                return NotFound($"No existe el paciente con id {id}");

            return Ok(paciente);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPaciente([FromBody] CreatePacienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            { 
                var nuevoPaciente = await _pacienteService.CrearPacienteAsync(dto);
                return Ok(nuevoPaciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el paciente: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPaciente(int id, [FromBody] UpdatePacienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _pacienteService.ActualizarPacienteAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPaciente(int id)
        {
            try
            {
                await _pacienteService.EliminarPacienteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
