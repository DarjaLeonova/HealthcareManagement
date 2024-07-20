using HealthcareManagement.Data;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagement.Tests.Repositories
{
    public class PatientRepositoryTests : IDisposable
    {
        private readonly HealthcareDbContext _context;
        private readonly PatientRepository _repository;

        public PatientRepositoryTests()
        {
            _context = GetInMemoryDbContext();
            _repository = new PatientRepository(_context);

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
            if (!_context.Patients.Any())
            {
                _context.Patients.AddRange(
                    new Patient { Id = 1, Name = "John Doe", DateOfBirth = new DateTime(1980, 1, 1), Address = "123 Main St", PhoneNumber = "123-456-7890" },
                    new Patient { Id = 2, Name = "Jane Smith", DateOfBirth = new DateTime(1990, 2, 2), Address = "456 Elm St", PhoneNumber = "098-765-4321" }
                );

                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAllPatientsAsync_ReturnsAllPatients()
        {
            // Act
            var patients = await _repository.GetAllPatientsAsync();

            // Assert
            Assert.Equal(2, patients.Count());
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsCorrectPatient()
        {
            // Act
            var patient = await _repository.GetPatientByIdAsync(1);

            // Assert
            Assert.NotNull(patient);
            Assert.Equal(1, patient.Id);
        }

        [Fact]
        public async Task AddPatientAsync_AddsPatient()
        {
            // Arrange
            var newPatient = new Patient { Id = 3, Name = "Samuel Green", DateOfBirth = new DateTime(2000, 3, 3), Address = "789 Oak St", PhoneNumber = "321-654-9870" };

            // Act
            await _repository.AddPatientAsync(newPatient);
            var patients = await _repository.GetAllPatientsAsync();

            // Assert
            Assert.Equal(3, patients.Count());
            Assert.Contains(patients, p => p.Id == 3);
        }

        [Fact]
        public async Task UpdatePatientAsync_UpdatesPatient()
        {
            // Arrange
            var patientToUpdate = await _repository.GetPatientByIdAsync(1);
            patientToUpdate.Address = "Updated Address";

            // Act
            await _repository.UpdatePatientAsync(patientToUpdate);
            var updatedPatient = await _repository.GetPatientByIdAsync(1);

            // Assert
            Assert.Equal("Updated Address", updatedPatient.Address);
        }

        [Fact]
        public async Task DeletePatientAsync_DeletesPatient()
        {
            // Act
            await _repository.DeletePatientAsync(1);
            var patient = await _repository.GetPatientByIdAsync(1);

            // Assert
            Assert.Null(patient);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}