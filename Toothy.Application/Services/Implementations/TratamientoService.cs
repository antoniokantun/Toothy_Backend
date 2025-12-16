using Toothy.Application.DTOs.Tratamientos;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class TratamientoService : ITratamientoService
    {
        private readonly IGenericRepository<Tratamiento> _tratamientoRepository;

        public TratamientoService(IGenericRepository<Tratamiento> tratamientoRepository)
        {
            _tratamientoRepository = tratamientoRepository;
        }

        public async Task<IEnumerable<Tratamiento>> ObtenerTodosTratamientoAsync()
        {
            return await _tratamientoRepository.ObtenerTodosAsync();
        }

        public async Task<Tratamiento> CrearTratamientoAsync(CreateTratamientoDto dto)
        {
            var nuevoTratamiento = new Tratamiento
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CostoBase = dto.CostoBase
            };

            var tratamientoCreado = await _tratamientoRepository.AgregarAsync(nuevoTratamiento);
            return tratamientoCreado;

        }

        
    }
}
