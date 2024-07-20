using HealthcareManagement.Business.Services;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories.Interfaces;
using Moq;

namespace HealthcareManagement.Tests.Services
{
    public class PrescriptionServiceTests
    {
        private readonly Mock<IPrescriptionRepository> _mockPrescriptionRepository;
        private readonly PrescriptionService _prescriptionService;

        public PrescriptionServiceTests()
        {
            _mockPrescriptionRepository = new Mock<IPrescriptionRepository>();
            _prescriptionService = new PrescriptionService(_mockPrescriptionRepository.Object);
        }

        [Fact]
        public async Task GetAllPrescriptionsAsync_ReturnsAllPrescriptions()
        {
            // Arrange
            var mockPrescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, MedicationName = "Med1", Dosage = "1 pill", Instructions = "Take one pill daily" },
                new Prescription { Id = 2, PatientId = 2, MedicationName = "Med2", Dosage = "2 pills", Instructions = "Take two pills daily" }
            };

            _mockPrescriptionRepository.Setup(repo => repo.GetAllPrescriptionsAsync()).ReturnsAsync(mockPrescriptions);

            // Act
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();

            // Assert
            Assert.Equal(2, prescriptions.Count());
        }

        [Fact]
        public async Task GetPrescriptionByIdAsync_ReturnsCorrectPrescription()
        {
            // Arrange
            var mockPrescription = new Prescription { Id = 1, PatientId = 1, MedicationName = "Med1", Dosage = "1 pill", Instructions = "Take one pill daily" };

            _mockPrescriptionRepository.Setup(repo => repo.GetPrescriptionByIdAsync(1)).ReturnsAsync(mockPrescription);

            // Act
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(1);

            // Assert
            Assert.NotNull(prescription);
            Assert.Equal(1, prescription.Id);
        }

        [Fact]
        public async Task AddPrescriptionAsync_AddsPrescription()
        {
            // Arrange
            var newPrescription = new Prescription { Id = 3, PatientId = 3, MedicationName = "Med3", Dosage = "1 pill", Instructions = "Take one pill daily" };

            _mockPrescriptionRepository.Setup(repo => repo.AddPrescriptionAsync(newPrescription)).Returns(Task.CompletedTask);

            // Act
            await _prescriptionService.AddPrescriptionAsync(newPrescription);

            // Assert
            _mockPrescriptionRepository.Verify(repo => repo.AddPrescriptionAsync(newPrescription), Times.Once);
        }

        [Fact]
        public async Task UpdatePrescriptionAsync_UpdatesPrescription()
        {
            // Arrange
            var existingPrescription = new Prescription { Id = 1, PatientId = 1, MedicationName = "Med1", Dosage = "1 pill", Instructions = "Take one pill daily" };

            _mockPrescriptionRepository.Setup(repo => repo.UpdatePrescriptionAsync(existingPrescription)).Returns(Task.CompletedTask);

            // Act
            await _prescriptionService.UpdatePrescriptionAsync(existingPrescription);

            // Assert
            _mockPrescriptionRepository.Verify(repo => repo.UpdatePrescriptionAsync(existingPrescription), Times.Once);
        }

        [Fact]
        public async Task DeletePrescriptionAsync_DeletesPrescription()
        {
            // Arrange
            _mockPrescriptionRepository.Setup(repo => repo.DeletePrescriptionAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _prescriptionService.DeletePrescriptionAsync(1);

            // Assert
            _mockPrescriptionRepository.Verify(repo => repo.DeletePrescriptionAsync(1), Times.Once);
        }
    }
}