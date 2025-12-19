using Toothy.Application.DTOs.Odontologos;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces
{
    public interface IOdontologoService
    {
        Task<IEnumerable<Odontologo>> ObtenerTodosOdontologoAsync();
        Task<Odontologo?> ObtenerOdontologoPorIdAsync(int id);
        Task<Odontologo> RegistrarOdontologoAsync(CreateOdontologoDto dto);
        Task ActualizarOdontologoAsync(int id, UpdateOdontologoDto dto);
        Task EliminarOdontologoAsync(int id);
    }
}
