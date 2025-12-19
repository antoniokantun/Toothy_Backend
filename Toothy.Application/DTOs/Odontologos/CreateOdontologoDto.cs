namespace Toothy.Application.DTOs.Odontologos
{
    public class CreateOdontologoDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Especialidad { get; set; }
        public string? Telefono { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string NumeroCedulaProfesional { get; set; }
    }
}
