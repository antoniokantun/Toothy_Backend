namespace Toothy.Application.DTOs.Recepcionistas
{
    public class CreateRecepcionistaDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
    }
}
