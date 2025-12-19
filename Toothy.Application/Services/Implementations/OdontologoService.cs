using Toothy.Application.DTOs.Odontologos;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class OdontologoService : IOdontologoService
    {
        private readonly IGenericRepository<Odontologo> _odontologoRepository;
        public OdontologoService(IGenericRepository<Odontologo> odontologoRepository)
        {
            _odontologoRepository = odontologoRepository;
        }

        public async Task<IEnumerable<Odontologo>> ObtenerTodosOdontologoAsync()
        {
            return await _odontologoRepository.ObtenerTodosAsync();
        }

        public async Task<Odontologo?> ObtenerOdontologoPorIdAsync(int id)
        {
            return await _odontologoRepository.ObtenerPorIdAsync(id);
        }

        public async Task<Odontologo> RegistrarOdontologoAsync(CreateOdontologoDto dto)
        {
            var nuevoOdontologo = new Odontologo
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Especialidad = dto.Especialidad,
                Telefono = dto.Telefono,
                CorreoElectronico = dto.CorreoElectronico,
                NumeroCedulaProfesional = dto.NumeroCedulaProfesional
            };

            var odontologoCreado = await _odontologoRepository.AgregarAsync(nuevoOdontologo);
            return odontologoCreado;
        }

        public async Task ActualizarOdontologoAsync(int id, UpdateOdontologoDto dto)
        {
            var odontologExistente = await _odontologoRepository.ObtenerPorIdAsync(id);

            if (odontologExistente == null)
            {
                throw new KeyNotFoundException($"Odontologo con Id {id} no encontrado.");
            }

            odontologExistente.Nombre = dto.Nombre;
            odontologExistente.Apellido = dto.Apellido;
            odontologExistente.Especialidad = dto.Especialidad;
            odontologExistente.Telefono = dto.Telefono;
            odontologExistente.CorreoElectronico = dto.CorreoElectronico;
            odontologExistente.NumeroCedulaProfesional = dto.NumeroCedulaProfesional;

            await _odontologoRepository.ActualizarAsync(odontologExistente);

        }
        public async Task EliminarOdontologoAsync(int id)
        {
            var odontologoExistente = await _odontologoRepository.ObtenerPorIdAsync(id);
            if (odontologoExistente == null)
            {
                throw new KeyNotFoundException($"Odontologo con Id {id} no encontrado.");
            }
            await _odontologoRepository.EliminarAsync(id);
        }
    }
}
