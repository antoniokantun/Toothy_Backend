using Toothy.Application.DTOs.Citas;
using Toothy.Application.Services.Interfaces;
using Toothy.Domain.Entities;
using Toothy.Domain.Interfaces;

namespace Toothy.Application.Services.Implementations
{
    public class CitaService : ICitaService
    {
        private readonly IGenericRepository<Cita> _citaRepository;
        private readonly IGenericRepository<Paciente> _pacienteRepository;
        private readonly IGenericRepository<Tratamiento> _tratamientoRepository;
        private readonly IGenericRepository<Odontologo> _odontologoRepository;

        public CitaService(
            IGenericRepository<Cita> citaRepository,
            IGenericRepository<Paciente> pacienteRepository,
            IGenericRepository<Tratamiento> tratamientoRepository,
            IGenericRepository<Odontologo> odontologoRepository)
        {
            _citaRepository = citaRepository;
            _pacienteRepository = pacienteRepository;
            _tratamientoRepository = tratamientoRepository;
            _odontologoRepository = odontologoRepository;
        }
        public async Task<Cita> RegistrarCitaAsync(CreateCitaDto dto)
        {
            var paciente = await _pacienteRepository.ObtenerPorIdAsync(dto.PacienteId);
            if (paciente == null)
                throw new KeyNotFoundException($"Paciente con ID {dto.PacienteId}no encontrado");

            var odontologo = await _odontologoRepository.ObtenerPorIdAsync(dto.OdontologoId);
            if (odontologo == null)
                throw new KeyNotFoundException($"El odontólogo con ID {dto.OdontologoId} no existe.");

            var nuevaCita = new Cita
            {
                PacienteId = dto.PacienteId,
                OdontologoId = dto.OdontologoId,
                FechaHoraInicio = dto.FechaHoraInicio,
                // Lógica simple: Duración de 1 hora por defecto (puedes mejorar esto sumando tiempos de tratamientos)
                FechaHoraFin = dto.FechaHoraInicio.AddHours(1),
                NotasCita = dto.NotasCita,
                Estado = EstadoCita.Agendada,
                EstadoPago = EstadoPago.Pendiente,
                CreatedAt = DateTime.UtcNow
            };

            decimal costoCitaAcumulado = 0;

            foreach (var tratamiento in dto.Tratamientos)
            {
                var itemTratamiento = await _tratamientoRepository.ObtenerPorIdAsync(tratamiento.TratamientoId);

                if (itemTratamiento == null)
                    throw new KeyNotFoundException(message: $"El tratamiento ID {tratamiento.TratamientoId} no existe");

                decimal precioUnitarioCita = itemTratamiento.CostoBase;
                decimal subtotal = precioUnitarioCita * tratamiento.Cantidad;
                costoCitaAcumulado += subtotal;

                var detalle = new DetalleCita
                {
                    TratamientoId = itemTratamiento.IdTratamiento,
                    Cantidad = tratamiento.Cantidad,
                    PrecioUnitario = precioUnitarioCita,
                    Subtotal = subtotal,
                    Observaciones = tratamiento.Observaciones,
                    CreatedAt = DateTime.UtcNow
                };

                nuevaCita.Detalles.Add(detalle);
            }

            nuevaCita.Total = costoCitaAcumulado;

            return await _citaRepository.AgregarAsync(nuevaCita);
        }
    }
}
