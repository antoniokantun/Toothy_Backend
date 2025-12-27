using Moq;
using Toothy.Application.DTOs.Usuarios;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Toothy.UnitTests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IGenericRepository<Usuario>> _mockUsuarioRepository;
        private readonly UsuarioService _usuarioService;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public UsuarioServiceTests()
        {
            _mockUsuarioRepository = new Mock<IGenericRepository<Usuario>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(c => c["JwtSettings:SecretKey"]).Returns("una-clave-secreta-muy-larga-para-pruebas");
            _mockConfiguration.Setup(c => c["JwtSettings:Issuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(c => c["JwtSettings:Audience"]).Returns("TestAudience");
            _mockConfiguration.Setup(c => c.GetValue<double>("JwtSettings:AccessTokenExpirationMinutes", 0)).Returns(15);
            _mockConfiguration.Setup(c => c.GetValue<int>("JwtSettings:RefreshTokenExpirationDays", 0)).Returns(7);

            _usuarioService = new UsuarioService(_mockUsuarioRepository.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task RegistrarAsync_DeberiaHashearPassword()
        {

            var dto = new CreateUsuarioDto
            {
                Username = "admin",
                Password = "Password123",
                Rol = Rol.Administrador
            };

            Usuario usuarioGuardado = null!;


            _mockUsuarioRepository.Setup(r => r.AgregarAsync(It.IsAny<Usuario>()))
                .Callback<Usuario>(u => usuarioGuardado = u)
                .ReturnsAsync((Usuario u) => { u.IdUsuario = 1; return u; });

            _mockUsuarioRepository.Setup(r => r.ObtenerTodosAsync()).ReturnsAsync(new List<Usuario>());

            var resultado = await _usuarioService.RegistrarUsuarioAsync(dto);

            Assert.NotNull(resultado);
            Assert.Equal("admin", resultado.Username);

            Assert.NotEqual("Password123", usuarioGuardado.PasswordHash);

            Assert.StartsWith("$2", usuarioGuardado.PasswordHash);
        }
    }
}
