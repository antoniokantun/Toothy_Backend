using Toothy.Domain.Entities;

namespace Toothy.Application.DTOs.Usuarios
{
    public class CreateUsuarioDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public Rol Rol { get; set; }
    }
}
