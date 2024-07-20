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
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _mockPatientService;
        private readonly Mock<IRouteService> _mockRouteService;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _mockPatientService = new Mock<IPatientService>();
            _mockRouteService = new Mock<IRouteService>();
            _controller = new PatientsController(_mockPatientService.Object, _mockRouteService.Object);
        }

        [Fact]
        public async Task GetPatients_ReturnsOkResult_WithListOfPatients()
        {
            // Arrange
            var mockPatients = new List<Patient>
            {
                new Patient { Id = 1, Name = "John Doe", DateOfBirth = System.DateTime.Now.AddYears(-30), Address = "123 Elm Street", PhoneNumber = "555-1234" },
                new Patient { Id = 2, Name = "Jane Smith", DateOfBirth = System.DateTime.Now.AddYears(-25), Address = "456 Oak Avenue", PhoneNumber = "555-5678" }
            };

            _mockPatientService.Setup(service => service.GetAllPatientsAsync()).ReturnsAsync(mockPatients);

            // Act
            var result = await _controller.GetPatients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var patients = Assert.IsAssignableFrom<IEnumerable<Patient>>(okResult.Value);
            Assert.Equal(2, patients.Count());
        }

        [Fact]
        public async Task GetPatient_ReturnsOkResult_WithPatient()
        {
            // Arrange
            var mockPatient = new Patient { Id = 1, Name = "John Doe", DateOfBirth = System.DateTime.Now.AddYears(-30), Address = "123 Elm Street", PhoneNumber = "555-1234" };

            _mockPatientService.Setup(service => service.GetPatientByIdAsync(1)).ReturnsAsync(mockPatient);

            // Act
            var result = await _controller.GetPatient(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var patient = Assert.IsType<Patient>(okResult.Value);
            Assert.Equal(1, patient.Id);
        }

        [Fact]
        public async Task GetPatient_ReturnsNotFound_WhenPatientDoesNotExist()
        {
            // Arrange
            _mockPatientService.Setup(service => service.GetPatientByIdAsync(1)).ReturnsAsync((Patient)null);

            // Act
            var result = await _controller.GetPatient(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddPatient_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newPatient = new Patient { Id = 3, Name = "Alice Brown", DateOfBirth = System.DateTime.Now.AddYears(-40), Address = "789 Pine Road", PhoneNumber = "555-7890" };

            _mockPatientService.Setup(service => service.AddPatientAsync(newPatient)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddPatient(newPatient);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetPatient), createdAtActionResult.ActionName);
            var patient = Assert.IsType<Patient>(createdAtActionResult.Value);
            Assert.Equal(3, patient.Id);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsNoContent()
        {
            // Arrange
            var existingPatient = new Patient { Id = 1, Name = "John Doe", DateOfBirth = System.DateTime.Now.AddYears(-30), Address = "123 Elm Street", PhoneNumber = "555-1234" };

            _mockPatientService.Setup(service => service.UpdatePatientAsync(existingPatient)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePatient(1, existingPatient);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePatient_ReturnsNoContent()
        {
            // Arrange
            _mockPatientService.Setup(service => service.DeletePatientAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePatient(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetDirections_ReturnsOkResult_WithDirections()
        {
            // Arrange
            var origin = "52.516276,13.377704"; // Berlin
            var destination = "48.856613,2.352222"; // Paris
            var mockDirections = "Directions from Berlin to Paris";

            _mockRouteService.Setup(service => service.GetDirectionsAsync(origin, destination)).ReturnsAsync(mockDirections);

            // Act
            var result = await _controller.GetDirections(origin, destination);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var directions = Assert.IsType<string>(okResult.Value);
            Assert.Equal(mockDirections, directions);
        }
    }
}
