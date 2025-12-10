namespace Toothy.Domain.Entities
{
    public class DetalleCita : BaseEntity
    {
        public int IdDetalleCita { get; set; }

        public int CitaId { get; set; }
        public Cita? Cita { get; set; }

        public int TratamientoId { get; set; }
        public Tratamiento? Tratamiento { get; set; }

        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string? Observaciones { get; set; }
    }
}
