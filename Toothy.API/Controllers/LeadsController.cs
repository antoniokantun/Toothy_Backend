using Microsoft.AspNetCore.Mvc;
using Toothy.Application.DTOs.Leads;
using Toothy.Application.Services.Interfaces;

namespace Toothy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadsController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLead()
        {
            var leads = await _leadService.ObtenerTodosLeadAsync();
            return Ok(leads);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarLead([FromBody] CreateLeadDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await _leadService.RegistrarLeadAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
