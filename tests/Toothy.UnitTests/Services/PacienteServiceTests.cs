using Moq;
using Toothy.Application.DTOs.Pacientes;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.UnitTests.Services
{
    public class PacienteServiceTests
    {
        private readonly Mock<IGenericRepository<Paciente>> _mockPacienteRepository;
        private readonly PacienteService _pacienteService;

        public PacienteServiceTests()
        {
            _mockPacienteRepository = new Mock<IGenericRepository<Paciente>>();
            _pacienteService = new PacienteService(_mockPacienteRepository.Object);
        }

        [Fact]
        public async Task CrearAsync_DeberiaMapearCorrectamenteYGuardar()
        {
            var dto = new CreatePacienteDto
            {
                Nombre = "Tony",
                Apellido = "Stark",
                CorreoElectronico = "ironman@avengers.com",
                FechaNacimiento = new DateTime(1970, 5, 29),
                Genero = Genero.Masculino,
                ContactoEmergenciaNombre = "Pepper Potts",
                ContactoEmergenciaTelefono = "123-456-7890"
            };

            _mockPacienteRepository.Setup(r => r.AgregarAsync(It.IsAny<Paciente>()))
                .ReturnsAsync((Paciente p) =>
                {
                    p.IdPaciente = 1;
                    return p;
                });

            var nuevoPaciente = await _pacienteService.CrearPacienteAsync(dto);

            Assert.NotNull(nuevoPaciente);
            Assert.Equal(1, nuevoPaciente.IdPaciente);
            Assert.Equal("Tony", nuevoPaciente.Nombre);
            Assert.Equal("Stark", nuevoPaciente.Apellido);
            Assert.Equal(Genero.Masculino, nuevoPaciente.Genero);
            Assert.NotEqual(DateTime.MinValue, nuevoPaciente.FechaRegistro);
            _mockPacienteRepository.Verify( r => r.AgregarAsync(It.IsAny<Paciente>()), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_SiNoExiste_DeberiaLanzarExcepcion()
        {
            int idPacienteInexistente = 999;
            var dto = new UpdatePacienteDto
            {
                Nombre = "Bruce",
                Apellido = "Banner",
                CorreoElectronico = "hola",
                ContactoEmergenciaNombre = "Natasha Romanoff",
                ContactoEmergenciaTelefono = "987-654-3210",
            };

            _mockPacienteRepository.Setup(r => r.ObtenerPorIdAsync(idPacienteInexistente))
                .ReturnsAsync((Paciente?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _pacienteService.ActualizarPacienteAsync(idPacienteInexistente, dto));
        }
    }
}
