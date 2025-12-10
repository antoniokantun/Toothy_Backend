namespace Toothy.Domain.Entities
{
    public class Tratamiento : BaseEntity
    {
        public int IdTratamiento { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal CostoBase { get; set; }
    }
}
