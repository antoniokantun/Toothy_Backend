using Moq;
using Toothy.Application.DTOs.Tratamientos;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;
using Xunit;

namespace Toothy.UnitTests.Services;

public class TratamientoServiceTests
{
    private readonly Mock<IGenericRepository<Tratamiento>> _mockRepository;

    private readonly TratamientoService _service;

    public TratamientoServiceTests()
    {
        _mockRepository = new Mock<IGenericRepository<Tratamiento>>();

        _service = new TratamientoService(_mockRepository.Object);
    }

    [Fact]
    public async Task CrearAsync_DeberiaGuardarYRetornarTratamiento()
    {
        var dto = new CreateTratamientoDto
        {
            Nombre = "Blanqueamiento",
            CostoBase = 2500m,
            Descripcion = "Se blanquea los dientes en minutos"
        };

        _mockRepository.Setup(repo => repo.AgregarAsync(It.IsAny<Tratamiento>()))
            .ReturnsAsync((Tratamiento t) =>
            {
                t.IdTratamiento = 1; 
                return t;
            });

        var resultado = await _service.CrearTratamientoAsync(dto);

        Assert.NotNull(resultado);

        Assert.Equal("Blanqueamiento", resultado.Nombre);
        Assert.Equal(2500m, resultado.CostoBase);
        Assert.Equal(1, resultado.IdTratamiento);

        _mockRepository.Verify(repo => repo.AgregarAsync(It.IsAny<Tratamiento>()), Times.Once);
    }

    [Fact]
    public async Task ObtenerTodosAsync_DeberiaRetornarListaDeTratamientos()
    {
        var listaFalsa = new List<Tratamiento>
        {
            new Tratamiento { IdTratamiento = 1, Nombre = "Limpieza", CostoBase = 500 },
            new Tratamiento { IdTratamiento = 2, Nombre = "Resina", CostoBase = 800 }
        };

        _mockRepository.Setup(repo => repo.ObtenerTodosAsync())
            .ReturnsAsync(listaFalsa);

        var resultado = await _service.ObtenerTodosTratamientoAsync();

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
        Assert.Contains(resultado, t => t.Nombre == "Limpieza");
    }
}