using Toothy.Domain.Entities;

namespace Toothy.Application.DTOs.Pacientes
{
    public class CreatePacienteDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Genero Genero { get; set; }
        public string? Direccion { get; set; }
        public required string ContactoEmergenciaNombre { get; set; }
        public required string ContactoEmergenciaTelefono { get; set; }

    }
}
