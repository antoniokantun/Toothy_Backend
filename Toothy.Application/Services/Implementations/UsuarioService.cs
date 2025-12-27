using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Text;
using Toothy.Application.DTOs.Auth;
using Toothy.Application.DTOs.Usuarios;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<UsuarioResponseDto> RegistrarUsuarioAsync(CreateUsuarioDto dto)
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            if (usuarios.Any(u => u.Username == dto.Username))
            {
                throw new InvalidOperationException($"El usuario {dto.Username} ya existe en el sistema.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var nuevoUsuario = new Usuario
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                Rol = dto.Rol,
                EstaActivo = true,
                UltimaVezLogueado = DateTime.UtcNow
            };

            var usuarioGuardado = await _usuarioRepository.AgregarAsync(nuevoUsuario);

            return new UsuarioResponseDto
            {
                IdUsuario = usuarioGuardado.IdUsuario,
                Username = usuarioGuardado.Username,
                Rol = usuarioGuardado.Rol,
                EstaActivo = usuarioGuardado.EstaActivo
            };
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosUsuarioAsync()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();

            return usuarios.Select(u => new UsuarioResponseDto
            {
                IdUsuario = u.IdUsuario,
                Username = u.Username,
                Rol = u.Rol,
                EstaActivo = u.EstaActivo
            });
        }

        public async Task<Usuario?> BuscarPorUsernameAsync(string username)
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();

            return usuarios.FirstOrDefault(u => u.Username == username);
        }

        public async Task<TokenResponseDto> LoginUsuarioAsync(LoginDto dto)
        {
            var usuario = await BuscarPorUsernameAsync(dto.Username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                throw new UnauthorizedAccessException("Las credenciales son invalidas.");
            }

            var accessToken = GenerarAccessToken(usuario);
            var refreshToken = GenerarRefreshToken();

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                _configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationDays")
                );

            await _usuarioRepository.ActualizarAsync(usuario);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
            var username = principal.Identity?.Name;

            var usuario = await BuscarPorUsernameAsync(username!);

            if (usuario == null ||
                usuario.RefreshToken != dto.RefreshToken ||
                usuario.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token invalido o expirado.");
            }

            var newAccessToken = GenerarAccessToken(usuario);
            var newRefreshToken = GenerarRefreshToken();

            usuario.RefreshToken = newRefreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _usuarioRepository.ActualizarAsync(usuario);

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        private string GenerarAccessToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
                new Claim("Id", usuario.IdUsuario.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("JwtSettings:AccessTokenExpirationMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerarRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token inválido");
            }

            return principal;
        }
    }
}
