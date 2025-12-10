using Toothy.Application.DTOs.Leads;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces;

public interface ILeadService
{
    Task<Lead> RegistrarLeadAsync(CreateLeadDto dto);


    Task<IEnumerable<Lead>> ObtenerTodosLeadAsync();
}
