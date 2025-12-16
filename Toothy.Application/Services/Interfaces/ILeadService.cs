using Toothy.Application.DTOs.Leads;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces;

public interface ILeadService
{
    Task<IEnumerable<Lead>> ObtenerTodosLeadAsync();
    Task<Lead> RegistrarLeadAsync(CreateLeadDto dto);
}
