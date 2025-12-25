using Toothy.Application.DTOs.Pacientes;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces
{
    public interface IPacienteService
    {
        
        Task<IEnumerable<Paciente>> ObtenerTodosPacienteAsync();
        Task<Paciente?> ObtenerPacientePorIdAsync(int id);
        Task<Paciente> CrearPacienteAsync(CreatePacienteDto dto);
        Task ActualizarPacienteAsync(int id, UpdatePacienteDto dto);
        Task EliminarPacienteAsync(int id);
    }
}
