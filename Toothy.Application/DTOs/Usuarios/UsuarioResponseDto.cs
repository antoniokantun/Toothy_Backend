using Toothy.Domain.Entities;

namespace Toothy.Application.DTOs.Usuarios
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; } = string.Empty;
        public Rol Rol { get; set; }
        public bool EstaActivo { get; set; }
    }
}
