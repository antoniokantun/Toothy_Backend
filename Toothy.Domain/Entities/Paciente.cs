namespace Toothy.Domain.Entities;

public enum Genero
{
    Masculino = 0,
    Femenino = 1,
    Otro = 2
}

public class Paciente : BaseEntity
{
    public int IdPaciente { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public string? Telefono { get; set; }
    public required string CorreoElectronico { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public Genero Genero { get; set; }
    public string? Direccion { get; set; }
    public required string ContactoEmergenciaNombre { get; set; }
    public required string ContactoEmergenciaTelefono { get; set; }
    public DateTime FechaRegistro { get; set; }

    public HistorialMedico? HistorialMedico { get; set; }
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();

}


