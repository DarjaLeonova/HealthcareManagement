using HealthcareManagement.Business.Services.Interfaces;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories.Interfaces;

namespace HealthcareManagement.Business.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _appointmentRepository.GetAllAppointmentsAsync();
    }

    public async Task<Appointment> GetAppointmentByIdAsync(int id)
    {
        return await _appointmentRepository.GetAppointmentByIdAsync(id);
    }

    public async Task AddAppointmentAsync(Appointment appointment)
    {
        await _appointmentRepository.AddAppointmentAsync(appointment);
    }

    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        await _appointmentRepository.UpdateAppointmentAsync(appointment);
    }

    public async Task DeleteAppointmentAsync(int id)
    {
        await _appointmentRepository.DeleteAppointmentAsync(id);
    }
}