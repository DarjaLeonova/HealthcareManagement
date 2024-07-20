using HealthcareManagement.Business.Services.Interfaces;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories.Interfaces;

namespace HealthcareManagement.Business.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
    {
        return await _prescriptionRepository.GetAllPrescriptionsAsync();
    }

    public async Task<Prescription> GetPrescriptionByIdAsync(int id)
    {
        return await _prescriptionRepository.GetPrescriptionByIdAsync(id);
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        await _prescriptionRepository.AddPrescriptionAsync(prescription);
    }

    public async Task UpdatePrescriptionAsync(Prescription prescription)
    {
        await _prescriptionRepository.UpdatePrescriptionAsync(prescription);
    }

    public async Task DeletePrescriptionAsync(int id)
    {
        await _prescriptionRepository.DeletePrescriptionAsync(id);
    }
}