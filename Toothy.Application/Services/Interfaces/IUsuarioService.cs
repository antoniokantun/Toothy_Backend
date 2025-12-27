using Toothy.Application.DTOs.Auth;
using Toothy.Application.DTOs.Usuarios;
using Toothy.Domain.Entities;

namespace Toothy.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> RegistrarUsuarioAsync(CreateUsuarioDto dto);
        Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosUsuarioAsync();
        Task<Usuario?> BuscarPorUsernameAsync(string username);
        Task<TokenResponseDto> LoginUsuarioAsync(LoginDto dto);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto);
    }
}
