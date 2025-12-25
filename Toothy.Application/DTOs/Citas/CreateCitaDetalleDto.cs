namespace Toothy.Application.DTOs.Citas
{
    public class CreateCitaDetalleDto
    {
        public int TratamientoId { get; set; }
        public int Cantidad { get; set; } = 1;
        public string Observaciones { get; set; } = string.Empty;
    }
}
