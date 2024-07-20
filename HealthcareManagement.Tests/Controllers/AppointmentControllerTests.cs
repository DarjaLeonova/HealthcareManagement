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
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _mockAppointmentService;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockAppointmentService = new Mock<IAppointmentService>();
            _controller = new AppointmentsController(_mockAppointmentService.Object);
        }

        [Fact]
        public async Task GetAppointments_ReturnsOkResult_WithListOfAppointments()
        {
            // Arrange
            var mockAppointments = new List<Appointment>
            {
                new Appointment { Id = 1, DoctorName = "Dr. Smith", AppointmentDate = System.DateTime.Now, Description = "Routine Checkup" },
                new Appointment { Id = 2, DoctorName = "Dr. Jones", AppointmentDate = System.DateTime.Now.AddDays(1), Description = "Follow-up" }
            };

            _mockAppointmentService.Setup(service => service.GetAllAppointmentsAsync()).ReturnsAsync(mockAppointments);

            // Act
            var result = await _controller.GetAppointments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var appointments = Assert.IsAssignableFrom<IEnumerable<Appointment>>(okResult.Value);
            Assert.Equal(2, appointments.Count());
        }

        [Fact]
        public async Task GetAppointment_ReturnsOkResult_WithAppointment()
        {
            // Arrange
            var mockAppointment = new Appointment { Id = 1, DoctorName = "Dr. Smith", AppointmentDate = System.DateTime.Now, Description = "Routine Checkup" };

            _mockAppointmentService.Setup(service => service.GetAppointmentByIdAsync(1)).ReturnsAsync(mockAppointment);

            // Act
            var result = await _controller.GetAppointment(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var appointment = Assert.IsType<Appointment>(okResult.Value);
            Assert.Equal(1, appointment.Id);
        }

        [Fact]
        public async Task GetAppointment_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            _mockAppointmentService.Setup(service => service.GetAppointmentByIdAsync(1)).ReturnsAsync((Appointment)null);

            // Act
            var result = await _controller.GetAppointment(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddAppointment_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newAppointment = new Appointment { Id = 3, DoctorName = "Dr. Black", AppointmentDate = System.DateTime.Now, Description = "Initial Visit" };

            _mockAppointmentService.Setup(service => service.AddAppointmentAsync(newAppointment)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddAppointment(newAppointment);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetAppointment), createdAtActionResult.ActionName);
            var appointment = Assert.IsType<Appointment>(createdAtActionResult.Value);
            Assert.Equal(3, appointment.Id);
        }

        [Fact]
        public async Task UpdateAppointment_ReturnsNoContent()
        {
            // Arrange
            var existingAppointment = new Appointment { Id = 1, DoctorName = "Dr. Smith", AppointmentDate = System.DateTime.Now, Description = "Routine Checkup" };

            _mockAppointmentService.Setup(service => service.UpdateAppointmentAsync(existingAppointment)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateAppointment(1, existingAppointment);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAppointment_ReturnsNoContent()
        {
            // Arrange
            _mockAppointmentService.Setup(service => service.DeleteAppointmentAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAppointment(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
