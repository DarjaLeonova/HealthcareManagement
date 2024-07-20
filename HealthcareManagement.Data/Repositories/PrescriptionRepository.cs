using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagement.Data.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly HealthcareDbContext _context;

    public PrescriptionRepository(HealthcareDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
    {
        return await _context.Prescriptions.ToListAsync();
    }

    public async Task<Prescription> GetPrescriptionByIdAsync(int id)
    {
        return await _context.Prescriptions.FindAsync(id);
    }

    public async Task AddPrescriptionAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePrescriptionAsync(Prescription prescription)
    {
        _context.Prescriptions.Update(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePrescriptionAsync(int id)
    {
        var prescription = await _context.Prescriptions.FindAsync(id);
        if (prescription != null)
        {
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
        }
    }
}