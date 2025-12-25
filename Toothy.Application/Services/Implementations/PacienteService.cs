using Toothy.Application.DTOs.Pacientes;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class PacienteService : IPacienteService
    {
        private readonly IGenericRepository<Paciente> _pacienteRepository;

        public PacienteService(IGenericRepository<Paciente> pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IEnumerable<Paciente>> ObtenerTodosPacienteAsync()
        {
            return await _pacienteRepository.ObtenerTodosAsync();
        }

        public async Task<Paciente?> ObtenerPacientePorIdAsync(int id)
        {
            return await _pacienteRepository.ObtenerPorIdAsync(id);
        }

        public async Task<Paciente> CrearPacienteAsync(CreatePacienteDto dto)
        {
            var nuevoPaciente = new Paciente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                CorreoElectronico = dto.CorreoElectronico,
                Telefono = dto.Telefono,
                FechaNacimiento = dto.FechaNacimiento,
                Genero = dto.Genero,
                Direccion = dto.Direccion,
                ContactoEmergenciaNombre = dto.ContactoEmergenciaNombre,
                ContactoEmergenciaTelefono = dto.ContactoEmergenciaTelefono,
                FechaRegistro = DateTime.UtcNow
            };

            return await _pacienteRepository.AgregarAsync(nuevoPaciente);
        }

        public async Task ActualizarPacienteAsync(int id, UpdatePacienteDto dto)
        {
            var pacienteExistente = await _pacienteRepository.ObtenerPorIdAsync(id);
            if (pacienteExistente == null)
                throw new KeyNotFoundException($"Paciente con Id: {id} no fue encontrado.");

            pacienteExistente.Nombre = dto.Nombre;
            pacienteExistente.Apellido = dto.Apellido;
            pacienteExistente.CorreoElectronico = dto.CorreoElectronico;
            pacienteExistente.Telefono = dto.Telefono;
            pacienteExistente.FechaNacimiento = dto.FechaNacimiento;
            pacienteExistente.Genero = dto.Genero;
            pacienteExistente.Direccion = dto.Direccion;
            pacienteExistente.ContactoEmergenciaNombre = dto.ContactoEmergenciaNombre;
            pacienteExistente.ContactoEmergenciaTelefono = dto.ContactoEmergenciaTelefono;

            await _pacienteRepository.ActualizarAsync(pacienteExistente);
        }

        public async Task EliminarPacienteAsync(int id)
        {
            var pacienteExistente = await _pacienteRepository.ObtenerPorIdAsync(id);
            if (pacienteExistente == null)
                throw new KeyNotFoundException($"Paciente con Id: {id} no fue encontrado.");
            await _pacienteRepository.EliminarAsync(id);
        }

    }
}
