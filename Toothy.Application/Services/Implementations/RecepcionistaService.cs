using Toothy.Application.DTOs.Recepcionistas;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class RecepcionistaService : IRecepcionistaService
    {
        private readonly IGenericRepository<Recepcionista> _recepcionistaRepository;

        public RecepcionistaService(IGenericRepository<Recepcionista> recepcionistaRepository)
        {
            _recepcionistaRepository = recepcionistaRepository;
        }

        public async Task<IEnumerable<Recepcionista>> ObtenerTodosRecepcionistaAsync()
        {
            return await _recepcionistaRepository.ObtenerTodosAsync();
        }

        public async Task<Recepcionista?> ObtenerRecepcionistaPorIdAsync(int id)
        {
            return await _recepcionistaRepository.ObtenerPorIdAsync(id);
        }

        public async Task<Recepcionista> CrearRecepcionistaAsync(CreateRecepcionistaDto dto)
        {
            var nuevaRecepcionista = new Recepcionista
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                CorreoElectronico = dto.CorreoElectronico,
                Telefono = dto.Telefono
            };

            return await _recepcionistaRepository.AgregarAsync(nuevaRecepcionista);
        }

        public async Task ActualizarRecepcionistaAsync(int id, UpdateRecepcionistaDto dto)
        {
            var recepcionistaExistente = await _recepcionistaRepository.ObtenerPorIdAsync(id);

            if (recepcionistaExistente == null)
            {
                throw new KeyNotFoundException($"Recepcionista con ID {id} no encontrado.");
            }

            recepcionistaExistente.Nombre = dto.Nombre;
            recepcionistaExistente.Apellido = dto.Apellido;
            recepcionistaExistente.CorreoElectronico = dto.CorreoElectronico;
            recepcionistaExistente.Telefono = dto.Telefono;

            await _recepcionistaRepository.ActualizarAsync(recepcionistaExistente);
        }

        public async Task EliminarRecepcionistaAsync(int id)
        {
            var recepcionistaExistente = await _recepcionistaRepository.ObtenerPorIdAsync(id);
            if (recepcionistaExistente == null)
            {
                throw new KeyNotFoundException($"Recepcionista con ID {id} no encontrado.");
            }

            await _recepcionistaRepository.EliminarAsync(id);
        }
    }
}
