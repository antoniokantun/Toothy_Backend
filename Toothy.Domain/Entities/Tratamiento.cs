namespace Toothy.Domain.Entities
{
    public class Tratamiento
    {
        public int IdTratamiento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public int DuracionEstimadoMinutos { get; set; }
    }
}
