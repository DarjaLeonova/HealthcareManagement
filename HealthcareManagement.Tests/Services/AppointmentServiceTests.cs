using HealthcareManagement.Business.Services;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories.Interfaces;
using Moq;

namespace HealthcareManagement.Tests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockAppointmentRepository;
        private readonly AppointmentService _appointmentService;

        public AppointmentServiceTests()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>();
            _appointmentService = new AppointmentService(_mockAppointmentRepository.Object);
        }

        [Fact]
        public async Task GetAllAppointmentsAsync_ReturnsAllAppointments()
        {
            // Arrange
            var mockAppointments = new List<Appointment>
            {
                new Appointment { Id = 1, PatientId = 1, DoctorName = "Dr. Smith", AppointmentDate = System.DateTime.Now, Description = "Check-up" },
                new Appointment { Id = 2, PatientId = 2, DoctorName = "Dr. Brown", AppointmentDate = System.DateTime.Now, Description = "Follow-up" }
            };

            _mockAppointmentRepository.Setup(repo => repo.GetAllAppointmentsAsync()).ReturnsAsync(mockAppointments);

            // Act
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            // Assert
            Assert.Equal(2, appointments.Count());
        }

        [Fact]
        public async Task GetAppointmentByIdAsync_ReturnsCorrectAppointment()
        {
            // Arrange
            var mockAppointment = new Appointment { Id = 1, PatientId = 1, DoctorName = "Dr. Smith", AppointmentDate = System.DateTime.Now, Description = "Check-up" };

            _mockAppointmentRepository.Setup(repo => repo.GetAppointmentByIdAsync(1)).ReturnsAsync(mockAppointment);

            // Act
            var appointment = await _appointmentService.GetAppointmentByIdAsync(1);

            // Assert
            Assert.NotNull(appointment);
            Assert.Equal(1, appointment.Id);
        }

        [Fact]
        public async Task AddAppointmentAsync_AddsAppointment()
        {
            // Arrange
            var newAppointment = new Appointment { Id = 3, PatientId = 3, DoctorName = "Dr. White", AppointmentDate = System.DateTime.Now, Description = "Consultation" };

            _mockAppointmentRepository.Setup(repo => repo.AddAppointmentAsync(newAppointment)).Returns(Task.CompletedTask);

            // Act
            await _appointmentService.AddAppointmentAsync(newAppointment);

            // Assert
            _mockAppointmentRepository.Verify(repo => repo.AddAppointmentAsync(newAppointment), Times.Once);
        }

        [Fact]
        public async Task UpdateAppointmentAsync_UpdatesAppointment()
        {
            // Arrange
            var existingAppointment = new Appointment { Id = 1, PatientId = 1, DoctorName = "Dr. Smith", AppointmentDate = System.DateTime.Now, Description = "Check-up" };

            _mockAppointmentRepository.Setup(repo => repo.UpdateAppointmentAsync(existingAppointment)).Returns(Task.CompletedTask);

            // Act
            await _appointmentService.UpdateAppointmentAsync(existingAppointment);

            // Assert
            _mockAppointmentRepository.Verify(repo => repo.UpdateAppointmentAsync(existingAppointment), Times.Once);
        }

        [Fact]
        public async Task DeleteAppointmentAsync_DeletesAppointment()
        {
            // Arrange
            _mockAppointmentRepository.Setup(repo => repo.DeleteAppointmentAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _appointmentService.DeleteAppointmentAsync(1);

            // Assert
            _mockAppointmentRepository.Verify(repo => repo.DeleteAppointmentAsync(1), Times.Once);
        }
    }
}