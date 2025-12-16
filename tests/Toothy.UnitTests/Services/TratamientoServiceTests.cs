using Moq;
using Toothy.Application.DTOs.Tratamientos;
using Toothy.Application.Services.Implementations;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;
using Xunit;

namespace Toothy.UnitTests.Services;

public class TratamientoServiceTests
{
    // Mock del Repositorio: Es nuestro "actor" que finge ser la base de datos
    private readonly Mock<IGenericRepository<Tratamiento>> _mockRepository;

    // El Servicio real que vamos a probar
    private readonly TratamientoService _service;

    public TratamientoServiceTests()
    {
        // 1. Preparamos los mocks antes de cada prueba
        _mockRepository = new Mock<IGenericRepository<Tratamiento>>();

        // 2. Inyectamos el mock en el servicio real
        _service = new TratamientoService(_mockRepository.Object);
    }

    [Fact]
    public async Task CrearAsync_DeberiaGuardarYRetornarTratamiento()
    {
        // ARRANGE (Preparar)
        // Datos que enviaremos
        var dto = new CreateTratamientoDto
        {
            Nombre = "Blanqueamiento",
            CostoBase = 2500m,
            Descripcion = "Se blanquea los dientes en minutos"
        };

        // Configuramos el Mock: "Cuando alguien llame a AddAsync, devuelve un Tratamiento con ID 1"
        _mockRepository.Setup(repo => repo.AgregarAsync(It.IsAny<Tratamiento>()))
            .ReturnsAsync((Tratamiento t) =>
            {
                t.IdTratamiento = 1; // Simulamos que la BD le asignó ID 1
                return t;
            });

        // ACT (Actuar)
        var resultado = await _service.CrearTratamientoAsync(dto);

        // ASSERT (Verificar)
        // 1. Verificamos que el resultado no sea nulo
        Assert.NotNull(resultado);

        // 2. Verificamos que los datos sean los mismos
        Assert.Equal("Blanqueamiento", resultado.Nombre);
        Assert.Equal(2500m, resultado.CostoBase);
        Assert.Equal(1, resultado.IdTratamiento);

        // 3. (Muy importante) Verificamos que el servicio SÍ haya llamado al repositorio una vez
        _mockRepository.Verify(repo => repo.AgregarAsync(It.IsAny<Tratamiento>()), Times.Once);
    }

    [Fact]
    public async Task ObtenerTodosAsync_DeberiaRetornarListaDeTratamientos()
    {
        // ARRANGE
        // Preparamos una lista falsa de datos que "ya existen" en la BD
        var listaFalsa = new List<Tratamiento>
        {
            new Tratamiento { IdTratamiento = 1, Nombre = "Limpieza", CostoBase = 500 },
            new Tratamiento { IdTratamiento = 2, Nombre = "Resina", CostoBase = 800 }
        };

        // Configuramos el Mock: "Cuando pidan GetAllAsync, devuelve la lista falsa"
        _mockRepository.Setup(repo => repo.ObtenerTodosAsync())
            .ReturnsAsync(listaFalsa);

        // ACT
        var resultado = await _service.ObtenerTodosTratamientoAsync();

        // ASSERT
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count()); // Deben ser 2
        Assert.Contains(resultado, t => t.Nombre == "Limpieza"); // Debe existir "Limpieza"
    }
}