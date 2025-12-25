using Moq;
using Toothy.Application.DTOs.Citas;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.UnitTests.Services
{
    public class CitaServiceTests
    {
        private readonly Mock<IGenericRepository<Cita>> _mockCitaRepository;
        private readonly Mock<IGenericRepository<Tratamiento>> _mockTratamientoRepository;
        private readonly Mock<IGenericRepository<Paciente>> _mockPacienteRepository;
        private readonly Mock<IGenericRepository<Odontologo>> _mockOdontologoRepository;

        private readonly CitaService _citaService;

        public CitaServiceTests()
        {
            _mockCitaRepository = new Mock<IGenericRepository<Cita>>();
            _mockTratamientoRepository = new Mock<IGenericRepository<Tratamiento>>();
            _mockPacienteRepository = new Mock<IGenericRepository<Paciente>>();
            _mockOdontologoRepository = new Mock<IGenericRepository<Odontologo>>();

            _citaService = new CitaService(
                _mockCitaRepository.Object,
                _mockPacienteRepository.Object,
                _mockTratamientoRepository.Object,
                _mockOdontologoRepository.Object
            );
        }

        [Fact]
        public async Task CrearCita_FlujoExitoso_DeberiaCalcularTotalYGuardar()
        {
            var dto = new CreateCitaDto
            {
                PacienteId = 1,
                OdontologoId = 1,
                FechaHoraInicio = DateTime.Now.AddDays(1),
                Tratamientos = new List<CreateCitaDetalleDto>
                { 
                    new CreateCitaDetalleDto 
                    { 
                        TratamientoId = 10, 
                        Cantidad = 2, 
                        Observaciones = "Observacion 1" 
                    }
                }
            };

            _mockPacienteRepository.Setup(r => r.ObtenerPorIdAsync(1))
                .ReturnsAsync(new Paciente 
                { 
                    IdPaciente = 1, 
                    Nombre = "Jose", 
                    Apellido = "House", 
                    CorreoElectronico = "X", 
                    ContactoEmergenciaNombre = "x", 
                    ContactoEmergenciaTelefono = "x" 
                });

            _mockOdontologoRepository.Setup(r => r.ObtenerPorIdAsync(1))
                .ReturnsAsync(new Odontologo 
                { 
                    IdOdontologo = 1, 
                    Nombre = "Gregory", 
                    Apellido = "House", 
                    CorreoElectronico = "Y", 
                    Especialidad = "Z", 
                    NumeroCedulaProfesional = "X" 
                });

            decimal precioBaseTratamiento = 500m;

            _mockTratamientoRepository.Setup(r => r.ObtenerPorIdAsync(10))
                .ReturnsAsync(new Tratamiento
                {
                    IdTratamiento = 10,
                    Nombre = "Limpieza",
                    CostoBase = precioBaseTratamiento
                });

            Cita? citaGuardada = null;

            _mockCitaRepository.Setup(r => r.AgregarAsync(It.IsAny<Cita>()))
                .Callback<Cita>(c => citaGuardada = c)
                .ReturnsAsync((Cita c) => { c.IdCita = 777; return c; });

            var resultado = await _citaService.RegistrarCitaAsync(dto);

            Assert.NotNull(resultado);
            Assert.Equal(1000m, resultado.Total);
            Assert.Single(resultado.Detalles);
            Assert.Equal(1000m, resultado.Detalles.First().Subtotal);
            Assert.Equal(
                dto.FechaHoraInicio.AddHours(1).ToString("g"),
                resultado.FechaHoraFin.ToString("g")
            );
        }

        [Fact]
        public async Task CrearCita_SiPacienteNoExiste_DeberiaFallar()
        {
            var dto = new CreateCitaDto { PacienteId = 99 };

            _mockPacienteRepository.Setup(r => r.ObtenerPorIdAsync(99)).ReturnsAsync((Paciente?)null);

            var excepcion = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _citaService.RegistrarCitaAsync(dto));

            Assert.Contains("paciente", excepcion.Message);
        }

        [Fact]
        public async Task CrearCita_SiTratamientoNoExiste_DeberiaFallar()
        {
            var dto = new CreateCitaDto
            {
                PacienteId = 1,
                OdontologoId = 1,
                Tratamientos = new List<CreateCitaDetalleDto> { new CreateCitaDetalleDto { TratamientoId = 500 } }
            };

            _mockPacienteRepository.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(new Paciente { Nombre = "X", Apellido = "X", CorreoElectronico = "x", ContactoEmergenciaNombre = "x", ContactoEmergenciaTelefono = "x"});
            _mockOdontologoRepository.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(new Odontologo { Nombre = "X", Apellido = "X", Especialidad = "x", CorreoElectronico = "x", NumeroCedulaProfesional = "x"});

            _mockTratamientoRepository.Setup(r => r.ObtenerPorIdAsync(500)).ReturnsAsync((Tratamiento?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _citaService.RegistrarCitaAsync(dto));
        }
    }
}
