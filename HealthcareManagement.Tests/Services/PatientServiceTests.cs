using HealthcareManagement.Business.Services;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories.Interfaces;
using Moq;

namespace HealthcareManagement.Tests.Services
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _mockPatientRepository;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _mockPatientRepository = new Mock<IPatientRepository>();
            _patientService = new PatientService(_mockPatientRepository.Object);
        }

        [Fact]
        public async Task GetAllPatientsAsync_ReturnsAllPatients()
        {
            // Arrange
            var mockPatients = new List<Patient>
            {
                new Patient { Id = 1, Name = "John Doe", DateOfBirth = new System.DateTime(1980, 1, 1), Address = "123 Main St", PhoneNumber = "555-1234" },
                new Patient { Id = 2, Name = "Jane Smith", DateOfBirth = new System.DateTime(1990, 2, 2), Address = "456 Elm St", PhoneNumber = "555-5678" }
            };

            _mockPatientRepository.Setup(repo => repo.GetAllPatientsAsync()).ReturnsAsync(mockPatients);

            // Act
            var patients = await _patientService.GetAllPatientsAsync();

            // Assert
            Assert.Equal(2, patients.Count());
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsCorrectPatient()
        {
            // Arrange
            var mockPatient = new Patient { Id = 1, Name = "John Doe", DateOfBirth = new System.DateTime(1980, 1, 1), Address = "123 Main St", PhoneNumber = "555-1234" };

            _mockPatientRepository.Setup(repo => repo.GetPatientByIdAsync(1)).ReturnsAsync(mockPatient);

            // Act
            var patient = await _patientService.GetPatientByIdAsync(1);

            // Assert
            Assert.NotNull(patient);
            Assert.Equal(1, patient.Id);
        }

        [Fact]
        public async Task AddPatientAsync_AddsPatient()
        {
            // Arrange
            var newPatient = new Patient { Id = 3, Name = "Alice Johnson", DateOfBirth = new System.DateTime(2000, 3, 3), Address = "789 Oak St", PhoneNumber = "555-9999" };

            _mockPatientRepository.Setup(repo => repo.AddPatientAsync(newPatient)).Returns(Task.CompletedTask);

            // Act
            await _patientService.AddPatientAsync(newPatient);

            // Assert
            _mockPatientRepository.Verify(repo => repo.AddPatientAsync(newPatient), Times.Once);
        }

        [Fact]
        public async Task UpdatePatientAsync_UpdatesPatient()
        {
            // Arrange
            var existingPatient = new Patient { Id = 1, Name = "John Doe", DateOfBirth = new System.DateTime(1980, 1, 1), Address = "123 Main St", PhoneNumber = "555-1234" };

            _mockPatientRepository.Setup(repo => repo.UpdatePatientAsync(existingPatient)).Returns(Task.CompletedTask);

            // Act
            await _patientService.UpdatePatientAsync(existingPatient);

            // Assert
            _mockPatientRepository.Verify(repo => repo.UpdatePatientAsync(existingPatient), Times.Once);
        }

        [Fact]
        public async Task DeletePatientAsync_DeletesPatient()
        {
            // Arrange
            _mockPatientRepository.Setup(repo => repo.DeletePatientAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _patientService.DeletePatientAsync(1);

            // Assert
            _mockPatientRepository.Verify(repo => repo.DeletePatientAsync(1), Times.Once);
        }
    }
}