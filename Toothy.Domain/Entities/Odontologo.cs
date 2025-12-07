namespace Toothy.Domain.Entities;

public class Odontologo
{
    public int IdOdontologo { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Especialidad { get; set; }
    public string Telefono { get; set; } = string.Empty;
    public required string CorreoElectronico { get; set; }
    public required string NumeroCedulaProfesional { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
