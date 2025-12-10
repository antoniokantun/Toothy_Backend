namespace Toothy.Domain.Entities
{
    public enum EstadoLead
    {
        Nuevo = 0,
        Contactado = 1,
        Convertido = 2,
        Descartado = 3
    }
    public class Lead : BaseEntity
    {
        public int IdLead { get; set; }
        public required string Nombre { get; set; }
        public required  string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public EstadoLead Estado { get; set; } = EstadoLead.Nuevo;
        public string? Notas { get; set; }
    }
}
