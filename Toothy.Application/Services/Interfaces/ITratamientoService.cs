using Toothy.Application.DTOs.Tratamientos;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces
{
    public interface ITratamientoService
    {
        Task<IEnumerable<Tratamiento>> ObtenerTodosTratamientoAsync();
        Task<Tratamiento> CrearTratamientoAsync(CreateTratamientoDto dto);
    }
}
