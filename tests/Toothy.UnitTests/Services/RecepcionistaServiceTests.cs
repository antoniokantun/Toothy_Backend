using Moq;
using Toothy.Application.DTOs.Recepcionistas;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.UnitTests.Services
{
    public class RecepcionistaServiceTests
    {
        private readonly Mock<IGenericRepository<Recepcionista>> _mockRecepcionistaRepository;
        private readonly RecepcionistaService _recepcionistaService;

        public RecepcionistaServiceTests()
        {
            _mockRecepcionistaRepository = new Mock<IGenericRepository<Recepcionista>>();
            _recepcionistaService = new RecepcionistaService(_mockRecepcionistaRepository.Object);
        }

        [Fact]
        public async Task CrearAsync_DeberiaGuardarRecepcionista()
        {
            var dto = new CreateRecepcionistaDto
            {
                Nombre = "Veronica",
                Apellido = "Lopez",
                CorreoElectronico = "vero@gmail.com",
                Telefono = "5551234908"
            };

            _mockRecepcionistaRepository.Setup(r => r.AgregarAsync(It.IsAny<Recepcionista>()))
                .ReturnsAsync((Recepcionista r) => { r.IdRecepcionista = 1; return r; });

            var resultado = await _recepcionistaService.CrearRecepcionistaAsync(dto);

            Assert.Equal(1, resultado.IdRecepcionista);
            Assert.Equal("Veronica", resultado.Nombre);
            _mockRecepcionistaRepository.Verify(r => r.AgregarAsync(It.IsAny<Recepcionista>()), Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_SiNoExiste_DeberiaFallar()
        {
            _mockRecepcionistaRepository.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Recepcionista?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _recepcionistaService.EliminarRecepcionistaAsync(99));
        }
    }
}
