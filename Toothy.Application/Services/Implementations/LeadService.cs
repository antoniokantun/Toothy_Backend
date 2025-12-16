using Toothy.Application.DTOs.Leads;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class LeadService : ILeadService
    {
        private readonly IGenericRepository<Lead> _leadRepository;

        public LeadService(IGenericRepository<Lead> leadRepository)
        {
            _leadRepository = leadRepository;
        }
        public async Task<IEnumerable<Lead>> ObtenerTodosLeadAsync()
        {
            return await _leadRepository.ObtenerTodosAsync();
        }

        public async Task<Lead> RegistrarLeadAsync(CreateLeadDto dto)
        {
            var nuevoLead = new Lead
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                CorreoElectronico = dto.CorreoElectronico,
                Telefono = dto.Telefono,
                Notas = dto.Notas,
                FechaSolicitud = DateTime.UtcNow,
                Estado = EstadoLead.Nuevo
            };

            var leadCreado = await _leadRepository.AgregarAsync(nuevoLead);

            return leadCreado;
        }
    }
}
