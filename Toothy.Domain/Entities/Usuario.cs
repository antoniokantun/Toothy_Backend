namespace Toothy.Domain.Entities
{
    public enum Rol
    {
        Administrador = 0,
        Odontologo = 1,
        Recepcionista = 2
    }
    public class Usuario : BaseEntity
    {
        public int IdUsuario { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime UltimaVezLogueado { get; set; } = DateTime.UtcNow;
        public bool EstaActivo { get; set; } = true;
        public Rol Rol { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
