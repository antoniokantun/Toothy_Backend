namespace Toothy.Domain.Entities
{
    public enum EstadoCita
    {
        Agendada = 0,
        EnProgreso = 1,
        Completada = 2,
        Cancelada = 3
    }
    public class Cita
    {
        public int IdCita { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public EstadoCita Estado { get; set; } = EstadoCita.Agendada;
        public string? NotasCita { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        public int OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }
        public int TratamientoId { get; set; }
        public Tratamiento? Tratamiento { get; set; }

    }
}
