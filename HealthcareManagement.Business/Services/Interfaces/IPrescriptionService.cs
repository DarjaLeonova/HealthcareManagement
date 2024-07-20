using HealthcareManagement.Data.Models;

namespace HealthcareManagement.Business.Services.Interfaces;

public interface IPrescriptionService
{
    Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
    Task<Prescription> GetPrescriptionByIdAsync(int id);
    Task AddPrescriptionAsync(Prescription prescription);
    Task UpdatePrescriptionAsync(Prescription prescription);
    Task DeletePrescriptionAsync(int id);
}