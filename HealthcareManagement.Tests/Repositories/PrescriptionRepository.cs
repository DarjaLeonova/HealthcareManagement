using HealthcareManagement.Data;
using HealthcareManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using HealthcareManagement.Data.Models;

namespace HealthcareManagement.Tests.Repositories
{
    public class PrescriptionRepositoryTests : IDisposable
    {
        private readonly HealthcareDbContext _context;
        private readonly PrescriptionRepository _repository;

        public PrescriptionRepositoryTests()
        {
            _context = GetInMemoryDbContext();
            _repository = new PrescriptionRepository(_context);

            // Seed the database with initial data if needed
            SeedDatabase();
        }

        private HealthcareDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HealthcareDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new HealthcareDbContext(options);
        }

        private void SeedDatabase()
        {
            if (!_context.Prescriptions.Any())
            {
                _context.Prescriptions.AddRange(
                    new Prescription { Id = 1, PatientId = 1, MedicationName = "Med1", Dosage = "1 pill", Instructions = "Take one pill daily" },
                    new Prescription { Id = 2, PatientId = 2, MedicationName = "Med2", Dosage = "2 pills", Instructions = "Take two pills daily" }
                );

                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAllPrescriptionsAsync_ReturnsAllPrescriptions()
        {
            // Act
            var prescriptions = await _repository.GetAllPrescriptionsAsync();

            // Assert
            Assert.Equal(2, prescriptions.Count());
        }

        [Fact]
        public async Task GetPrescriptionByIdAsync_ReturnsCorrectPrescription()
        {
            // Act
            var prescription = await _repository.GetPrescriptionByIdAsync(1);

            // Assert
            Assert.NotNull(prescription);
            Assert.Equal(1, prescription.Id);
        }

        [Fact]
        public async Task AddPrescriptionAsync_AddsPrescription()
        {
            // Arrange
            var newPrescription = new Prescription { Id = 3, PatientId = 3, MedicationName = "Med3", Dosage = "1 pill", Instructions = "Take one pill daily" };

            // Act
            await _repository.AddPrescriptionAsync(newPrescription);
            var prescriptions = await _repository.GetAllPrescriptionsAsync();

            // Assert
            Assert.Equal(3, prescriptions.Count());
            Assert.Contains(prescriptions, p => p.Id == 3);
        }

        [Fact]
        public async Task UpdatePrescriptionAsync_UpdatesPrescription()
        {
            // Arrange
            var prescriptionToUpdate = await _repository.GetPrescriptionByIdAsync(1);
            prescriptionToUpdate.Dosage = "Updated Dosage";

            // Act
            await _repository.UpdatePrescriptionAsync(prescriptionToUpdate);
            var updatedPrescription = await _repository.GetPrescriptionByIdAsync(1);

            // Assert
            Assert.Equal("Updated Dosage", updatedPrescription.Dosage);
        }

        [Fact]
        public async Task DeletePrescriptionAsync_DeletesPrescription()
        {
            // Act
            await _repository.DeletePrescriptionAsync(1);
            var prescription = await _repository.GetPrescriptionByIdAsync(1);

            // Assert
            Assert.Null(prescription);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
