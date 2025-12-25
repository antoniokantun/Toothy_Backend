using Toothy.Application.DTOs.Citas;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces
{
    public interface ICitaService
    {
        Task<Cita> RegistrarCitaAsync(CreateCitaDto dto);
    }
}
