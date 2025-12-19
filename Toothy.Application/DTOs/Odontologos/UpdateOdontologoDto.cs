namespace Toothy.Application.DTOs.Odontologos
{
    public class UpdateOdontologoDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Especialidad { get; set; }
        public required string CorreoElectronico { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string NumeroCedulaProfesional { get; set; } = string.Empty;
    }
}
