namespace Toothy.Application.DTOs.Tratamientos
{
    public class CreateTratamientoDto
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal CostoBase { get; set; }
    }
}
