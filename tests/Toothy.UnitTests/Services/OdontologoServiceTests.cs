using Moq;
using Toothy.Application.DTOs.Odontologos;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.UnitTests.Services
{
    public class OdontologoServiceTests
    {
        private readonly Mock<IGenericRepository<Odontologo>> _mockRepo;
        private readonly OdontologoService _service;

        public OdontologoServiceTests()
        {
            _mockRepo = new Mock<IGenericRepository<Odontologo>>();

            _service = new OdontologoService(_mockRepo.Object);
        }

        [Fact]
        public async Task ObtenerPorId_SiExiste_DeberiaRetornarOdontologo()
        {
            int idPrueba = 1;
            var odontologoSimulado = new Odontologo
            {
                IdOdontologo = idPrueba,
                Nombre = "Juan",
                Apellido = "Perez",
                Especialidad = "Ortodoncia",
                CorreoElectronico = "test@test.com",
                NumeroCedulaProfesional = "123"
            };

            _mockRepo.Setup(r => r.ObtenerPorIdAsync(idPrueba)).ReturnsAsync(odontologoSimulado);

            var resultado = await _service.ObtenerOdontologoPorIdAsync(idPrueba);

            Assert.NotNull(resultado);
            Assert.Equal(idPrueba, resultado.IdOdontologo);
        }

        [Fact]
        public async Task ObtenerPorId_SiNoExiste_DeberiaRetornarNull()
        {
            int idInexistente = 99;
            _mockRepo.Setup(r => r.ObtenerPorIdAsync(idInexistente)).ReturnsAsync((Odontologo?)null);

            var resultado = await _service.ObtenerOdontologoPorIdAsync(idInexistente);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Actualizar_SiExiste_DeberiaLlamarUpdateEnRepo()
        {
            int id = 1;
            var odontologoExistente = new Odontologo
            {
                IdOdontologo = id,
                Nombre = "Viejo",
                Apellido = "Nombre",
                Especialidad = "General",
                CorreoElectronico = "old@test.com",
                NumeroCedulaProfesional = "000"
            };

            var dtoConCambios = new UpdateOdontologoDto
            {
                Nombre = "Nuevo",
                Apellido = "Apellido",
                Especialidad = "Cirujano",
                CorreoElectronico = "new@test.com",
                Telefono = "555",
                NumeroCedulaProfesional = "111"
            };

            _mockRepo.Setup(r => r.ObtenerPorIdAsync(id)).ReturnsAsync(odontologoExistente);

            await _service.ActualizarOdontologoAsync(id, dtoConCambios);

            _mockRepo.Verify(r => r.ActualizarAsync(It.IsAny<Odontologo>()), Times.Once);

            Assert.Equal("Nuevo", odontologoExistente.Nombre);
        }

        [Fact]
        public async Task Actualizar_SiNoExiste_DeberiaLanzarExcepcion()
        {
            int idInexistente = 99;
            var dto = new UpdateOdontologoDto { Nombre = "X", Apellido = "X", Especialidad = "X", CorreoElectronico = "X" };

            _mockRepo.Setup(r => r.ObtenerPorIdAsync(idInexistente)).ReturnsAsync((Odontologo?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.ActualizarOdontologoAsync(idInexistente, dto));

            _mockRepo.Verify(r => r.ActualizarAsync(It.IsAny<Odontologo>()), Times.Never);
        }


        [Fact]
        public async Task Eliminar_SiExiste_DeberiaLlamarDeleteEnRepo()
        {
            int id = 1;
            var odontologo = new Odontologo
            {
                IdOdontologo = id,
                Nombre = "Borrar",
                Apellido = "Borrar",
                Especialidad = "X",
                CorreoElectronico = "x",
                NumeroCedulaProfesional = "x"
            };

            _mockRepo.Setup(r => r.ObtenerPorIdAsync(id)).ReturnsAsync(odontologo);

            await _service.EliminarOdontologoAsync(id);

            _mockRepo.Verify(r => r.EliminarAsync(id), Times.Once);
        }

        [Fact]
        public async Task Eliminar_SiNoExiste_DeberiaLanzarExcepcion()
        {
            int idInexistente = 99;
            _mockRepo.Setup(r => r.ObtenerPorIdAsync(idInexistente)).ReturnsAsync((Odontologo?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.EliminarOdontologoAsync(idInexistente));

            _mockRepo.Verify(r => r.EliminarAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
