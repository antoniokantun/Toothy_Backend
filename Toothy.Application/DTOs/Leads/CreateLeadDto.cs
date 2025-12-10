namespace Toothy.Application.DTOs.Leads
{
    public class CreateLeadDto
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string Telefono { get; set; }
        public string? Notas { get; set; }
    }
}
