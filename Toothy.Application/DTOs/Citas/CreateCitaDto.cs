namespace Toothy.Application.DTOs.Citas
{
    public class CreateCitaDto
    {
        public int PacienteId { get; set; }
        public int OdontologoId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public string? NotasCita { get; set; }
        public List<CreateCitaDetalleDto> Tratamientos { get; set; } = new List<CreateCitaDetalleDto>();
    }
}
