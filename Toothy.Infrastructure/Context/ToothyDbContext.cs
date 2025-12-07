using Microsoft.EntityFrameworkCore;
using Toothy.Domain.Entities;

namespace Toothy.Infrastructure.Context
{
    public class ToothyDbContext : DbContext
    {
        public ToothyDbContext(DbContextOptions<ToothyDbContext> options): base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Odontologo> Odontologos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<HistorialMedico> HistorialesMedicos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<Lead> Leads { get; set; }
    }
}
