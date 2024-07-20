using HealthcareManagement.API.Controllers;
using HealthcareManagement.Business.Services.Interfaces;
using HealthcareManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthcareManagement.Tests.Controllers
{
    public class PrescriptionsControllerTests
    {
        private readonly Mock<IPrescriptionService> _mockPrescriptionService;
        private readonly PrescriptionsController _controller;

        public PrescriptionsControllerTests()
        {
            _mockPrescriptionService = new Mock<IPrescriptionService>();
            _controller = new PrescriptionsController(_mockPrescriptionService.Object);
        }

        [Fact]
        public async Task GetPrescriptions_ReturnsOkResult_WithListOfPrescriptions()
        {
            // Arrange
            var mockPrescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, MedicationName = "Medication A", Dosage = "10mg", Instructions = "Take once daily" },
                new Prescription { Id = 2, PatientId = 2, MedicationName = "Medication B", Dosage = "20mg", Instructions = "Take twice daily" }
            };

            _mockPrescriptionService.Setup(service => service.GetAllPrescriptionsAsync()).ReturnsAsync(mockPrescriptions);

            // Act
            var result = await _controller.GetPrescriptions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var prescriptions = Assert.IsAssignableFrom<IEnumerable<Prescription>>(okResult.Value);
            Assert.Equal(2, prescriptions.Count());
        }

        [Fact]
        public async Task GetPrescription_ReturnsOkResult_WithPrescription()
        {
            // Arrange
            var mockPrescription = new Prescription { Id = 1, PatientId = 1, MedicationName = "Medication A", Dosage = "10mg", Instructions = "Take once daily" };

            _mockPrescriptionService.Setup(service => service.GetPrescriptionByIdAsync(1)).ReturnsAsync(mockPrescription);

            // Act
            var result = await _controller.GetPrescription(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var prescription = Assert.IsType<Prescription>(okResult.Value);
            Assert.Equal(1, prescription.Id);
        }

        [Fact]
        public async Task GetPrescription_ReturnsNotFound_WhenPrescriptionDoesNotExist()
        {
            // Arrange
            _mockPrescriptionService.Setup(service => service.GetPrescriptionByIdAsync(1)).ReturnsAsync((Prescription)null);

            // Act
            var result = await _controller.GetPrescription(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddPrescription_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newPrescription = new Prescription { Id = 3, PatientId = 1, MedicationName = "Medication C", Dosage = "30mg", Instructions = "Take thrice daily" };

            _mockPrescriptionService.Setup(service => service.AddPrescriptionAsync(newPrescription)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddPrescription(newPrescription);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetPrescription), createdAtActionResult.ActionName);
            var prescription = Assert.IsType<Prescription>(createdAtActionResult.Value);
            Assert.Equal(3, prescription.Id);
        }

        [Fact]
        public async Task UpdatePrescription_ReturnsNoContent()
        {
            // Arrange
            var existingPrescription = new Prescription { Id = 1, PatientId = 1, MedicationName = "Medication A", Dosage = "10mg", Instructions = "Take once daily" };

            _mockPrescriptionService.Setup(service => service.UpdatePrescriptionAsync(existingPrescription)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePrescription(1, existingPrescription);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePrescription_ReturnsNoContent()
        {
            // Arrange
            _mockPrescriptionService.Setup(service => service.DeletePrescriptionAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePrescription(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}