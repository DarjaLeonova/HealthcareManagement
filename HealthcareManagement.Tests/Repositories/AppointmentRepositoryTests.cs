using HealthcareManagement.Data;
using HealthcareManagement.Data.Models;
using HealthcareManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagement.Tests.Repositories
{
    public class AppointmentRepositoryTests
    {
        private HealthcareDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<HealthcareDbContext>()
                .UseInMemoryDatabase(databaseName: "HealthcareTestDb")
                .Options;

            var context = new HealthcareDbContext(options);

            // Seed the database with initial data if needed
            if (!context.Appointments.Any())
            {
                context.Appointments.AddRange(
                    new Appointment { Id = 1, PatientId = 1, DoctorName = "Dr. Smith", AppointmentDate = DateTime.Now, Description = "Checkup" },
                    new Appointment { Id = 2, PatientId = 2, DoctorName = "Dr. Jones", AppointmentDate = DateTime.Now, Description = "Consultation" }
                );

                context.SaveChanges();
            }

            return context;
        }

        [Fact]
        public async Task GetAllAppointmentsAsync_ReturnsAllAppointments()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new AppointmentRepository(context);

            // Act
            var appointments = await repository.GetAllAppointmentsAsync();

            // Assert
            Assert.Equal(2, appointments.Count());
        }

        [Fact]
        public async Task GetAppointmentByIdAsync_ReturnsCorrectAppointment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new AppointmentRepository(context);

            // Act
            var appointment = await repository.GetAppointmentByIdAsync(1);

            // Assert
            Assert.NotNull(appointment);
            Assert.Equal(1, appointment.Id);
        }

        [Fact]
        public async Task AddAppointmentAsync_AddsAppointment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new AppointmentRepository(context);
            var newAppointment = new Appointment { Id = 3, PatientId = 3, DoctorName = "Dr. Brown", AppointmentDate = DateTime.Now, Description = "Follow-up" };

            // Act
            await repository.AddAppointmentAsync(newAppointment);
            var appointments = await repository.GetAllAppointmentsAsync();

            // Assert
            Assert.Equal(3, appointments.Count());
            Assert.Contains(appointments, a => a.Id == 3);
        }

        [Fact]
        public async Task UpdateAppointmentAsync_UpdatesAppointment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new AppointmentRepository(context);
            var appointmentToUpdate = await repository.GetAppointmentByIdAsync(1);
            appointmentToUpdate.Description = "Updated Checkup";

            // Act
            await repository.UpdateAppointmentAsync(appointmentToUpdate);
            var updatedAppointment = await repository.GetAppointmentByIdAsync(1);

            // Assert
            Assert.Equal("Updated Checkup", updatedAppointment.Description);
        }

        [Fact]
        public async Task DeleteAppointmentAsync_DeletesAppointment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new AppointmentRepository(context);

            // Act
            await repository.DeleteAppointmentAsync(1);
            var appointment = await repository.GetAppointmentByIdAsync(1);

            // Assert
            Assert.Null(appointment);
        }
    }
}