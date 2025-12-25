using Toothy.Application.DTOs.Recepcionistas;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces
{
    public interface IRecepcionistaService
    {
        Task<IEnumerable<Recepcionista>> ObtenerTodosRecepcionistaAsync();
        Task<Recepcionista?> ObtenerRecepcionistaPorIdAsync(int id);
        Task<Recepcionista> CrearRecepcionistaAsync(CreateRecepcionistaDto dto);
        Task ActualizarRecepcionistaAsync(int id, UpdateRecepcionistaDto dto);
        Task EliminarRecepcionistaAsync(int id);
    }
}
