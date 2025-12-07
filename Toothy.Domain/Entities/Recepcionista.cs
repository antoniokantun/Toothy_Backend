namespace Toothy.Domain.Entities
{
    public class Recepcionista
    {
        public int IdRecepcionista { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public required string CorreoElectronico { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
