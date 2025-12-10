namespace Toothy.Domain.Entities
{
    public class HistorialMedico : BaseEntity
    {
        public int IdHistorialMedico { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public required string TipoSangre { get; set; }
        public required string EnfermedadesCronicas { get; set; }
        public required string Alergias { get; set; }
        public required string MedicamentosActuales { get; set; }
        public bool EsFumador { get; set; }
        public bool EstaEmbarazada { get; set; }
        public string? Observaciones { get; set; }
    }
}
