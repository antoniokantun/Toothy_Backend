namespace Toothy.Domain.Entities
{
    public enum EstadoCita
    {
        Agendada = 0,
        EnProgreso = 1,
        Completada = 2,
        Cancelada = 3
    }
    public enum MetodoPago
    {
        NoDefinido = 0,
        Efectivo = 1,
        TarjetaCredito = 2,
        TarjetaDebito = 3,
        Transferencia = 4,
        SeguroDental = 5
    }

    public enum EstadoPago
    {
        Pendiente = 0,
        Pagado = 1,
        Parcial = 2
    }
    public class Cita : BaseEntity
    {
        public int IdCita { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public EstadoCita Estado { get; set; } = EstadoCita.Agendada;
        public string? NotasCita { get; set; }
        public MetodoPago MetodoPago { get; set; } = MetodoPago.NoDefinido;
        public EstadoPago EstadoPago { get; set; } = EstadoPago.Pendiente;
        public decimal Total { get; set; } = 0;
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public int OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }
        public ICollection<DetalleCita> Detalles { get; set; } = new List<DetalleCita>();

    }
}
