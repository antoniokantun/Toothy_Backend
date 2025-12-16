using Microsoft.EntityFrameworkCore;
using Toothy.Domain.Entities;

namespace Toothy.Infrastructure.Context
{
    public class ToothyDbContext : DbContext
    {
        public ToothyDbContext(DbContextOptions<ToothyDbContext> options): base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<DetalleCita> DetallesCita { get; set; }
        public DbSet<Odontologo> Odontologos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<HistorialMedico> HistorialesMedicos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<Lead> Leads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //usuario
            modelBuilder.Entity<Usuario>( entity =>
            { 
                entity.HasKey(u => u.IdUsuario);
                entity.Property(u => u.Username)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(u => u.PasswordHash)
                    .HasMaxLength(500)
                    .IsRequired();
            });


            // Lead
            modelBuilder.Entity<Lead>(entity =>
            { 
                entity.HasKey(l => l.IdLead);
                entity.Property(l => l.Nombre)
                    .HasMaxLength(100);
                entity.Property(l => l.Apellido)
                    .HasMaxLength(100);
                entity.Property(l => l.Telefono)
                    .HasMaxLength(20);
                entity.Property(l => l.CorreoElectronico)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(l => l.FechaSolicitud)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(l => l.CorreoElectronico);
            });


            // Tratamiento
            modelBuilder.Entity<Tratamiento>(entity =>
            {
                entity.HasKey(t => t.IdTratamiento);

                entity.Property(t => t.Nombre).HasMaxLength(100);
                entity.Property(t => t.Descripcion).HasMaxLength(500);

                entity.Property(t => t.CostoBase)
                    .HasColumnType("decimal(18,2)");
            });

            // Paciente
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(p => p.IdPaciente);

                entity.Property(p => p.Nombre).HasMaxLength(100);
                entity.Property(p => p.Apellido).HasMaxLength(100);
                entity.Property(p => p.Telefono).HasMaxLength(20);
                entity.Property(p => p.Direccion).HasMaxLength(250);
                entity.Property(p => p.ContactoEmergenciaNombre).HasMaxLength(100);
                entity.Property(p => p.ContactoEmergenciaTelefono).HasMaxLength(20);

                entity.Property(p => p.CorreoElectronico)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasIndex(p => p.CorreoElectronico).IsUnique();
            });

            // Odontologo
            modelBuilder.Entity<Odontologo>(entity =>
            {
                entity.HasKey(o => o.IdOdontologo);

                entity.Property(o => o.Nombre).HasMaxLength(100);
                entity.Property(o => o.Apellido).HasMaxLength(100);
                entity.Property(o => o.Especialidad).HasMaxLength(100);
                entity.Property(o => o.Telefono).HasMaxLength(20);

                entity.Property(o => o.NumeroCedulaProfesional)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.HasIndex(o => o.NumeroCedulaProfesional).IsUnique();
                entity.HasIndex(o => o.CorreoElectronico).IsUnique();

                entity.HasOne(o => o.Usuario)
                    .WithMany()
                    .HasForeignKey(o => o.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Recepcionista
            modelBuilder.Entity<Recepcionista>(entity =>
            {
                entity.HasKey(r => r.IdRecepcionista);
                entity.Property(r => r.Nombre).HasMaxLength(100);
                entity.Property(r => r.Apellido).HasMaxLength(100);
                entity.Property(r => r.Telefono).HasMaxLength(20);
                entity.Property(r => r.CorreoElectronico).HasMaxLength(150);

                entity.HasOne(r => r.Usuario)
                    .WithMany()
                    .HasForeignKey(r => r.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // HistorialMedico
            modelBuilder.Entity<HistorialMedico>(entity =>
            {
                entity.HasKey(h => h.IdHistorialMedico);

                entity.Property(h => h.TipoSangre).HasMaxLength(10);

                entity.HasOne(h => h.Paciente)
                    .WithOne(p => p.HistorialMedico)
                    .HasForeignKey<HistorialMedico>(h => h.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cita
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(c => c.IdCita);

                entity.Property(c => c.Total).HasColumnType("decimal(18,2)");
                entity.Property(c => c.NotasCita).HasMaxLength(500);

                entity.HasOne(c => c.Paciente)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(c => c.PacienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Odontologo)
                    .WithMany(o => o.Citas)
                    .HasForeignKey(c => c.OdontologoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // DetalleCita
            modelBuilder.Entity<DetalleCita>(entity =>
            {
                entity.HasKey(d => d.IdDetalleCita);

                entity.Property(d => d.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(d => d.Subtotal).HasColumnType("decimal(18,2)");
                entity.Property(d => d.Observaciones).HasMaxLength(250);

                entity.HasOne(d => d.Cita)
                    .WithMany(c => c.Detalles)
                    .HasForeignKey(d => d.CitaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Tratamiento)
                    .WithMany()
                    .HasForeignKey(d => d.TratamientoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
