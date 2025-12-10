namespace Toothy.Domain.Entities
{
    public class Recepcionista : BaseEntity
    {
        public int IdRecepcionista { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Telefono { get; set; }
        public required string CorreoElectronico { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
