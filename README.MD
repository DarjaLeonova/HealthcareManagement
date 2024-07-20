# Healthcare Management Application

## Overview

The Healthcare Management Application is a web-based system built using ASP.NET Core and REST services. It allows healthcare providers to manage patient records, schedule appointments, and prescribe medications. The application integrates with third-party services, such as OpenRouteService, to provide directions to healthcare facilities.

## Features

- Manage patient records
- Schedule and manage appointments
- Prescribe medications
- Get directions to healthcare facilities using OpenRouteService

## Project Structure

- **HealthcareManagement.API**: Contains the API controllers and routes.
- **HealthcareManagement.Business**: Contains the business logic and service implementations.
- **HealthcareManagement.Data**: Contains the data access layer and Entity Framework Core configurations.

## Getting Started

### Prerequisites

- [.NET SDK 6.x](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other IDE/editor of your choice
- [Postman](https://www.postman.com/) or any API testing tool (optional but recommended for testing endpoints)

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/healthcare-management-app.git
   cd healthcare-management-app
2. **Restore Dependencies**
   Navigate to the project directory and restore the NuGet packages:
   ```bash
   dotnet restore
3. **Update Configuration**
   Update the appsettings.json file in HealthcareManagement.API with your OpenRouteService API key:
   ```json
   "OpenRouteService": {
    "ApiKey": "YOUR_API_KEY"
    }
4. **Run Migrations**
   Apply migrations to set up the database schema:
    ```bash
    dotnet ef database update --project HealthcareManagement.Data   
5. **Run the Application**
   Start the application using:
   ```bash
   dotnet run --project HealthcareManagement.API

## API Endpoints
### Patients 
#### GET /api/patients - Retrieve all patients
#### GET /api/patients/{id} - Retrieve a patient by ID
#### POST /api/patients - Add a new patient
#### PUT /api/patients/{id} - Update a patient
#### DELETE /api/patients/{id} - Delete a patient
#### GET /api/patients/directions - Get directions to a healthcare facility
### Appointments
#### GET /api/appointments - Retrieve all appointments
#### GET /api/appointments/{id} - Retrieve an appointment by ID
#### POST /api/appointments - Add a new appointment
#### PUT /api/appointments/{id} - Update an appointment
#### DELETE /api/appointments/{id} - Delete an appointment
### Prescriptions
#### GET /api/prescriptions - Retrieve all prescriptions
#### GET /api/prescriptions/{id} - Retrieve a prescription by ID
#### POST /api/prescriptions - Add a new prescription
#### PUT /api/prescriptions/{id} - Update a prescription
#### DELETE /api/prescriptions/{id} - Delete a prescription

## Running Tests

1. Run Tests
   ```bash
   dotnet test
2. View Code Coverage
   Run tests with code coverage:
    ```bash
   dotnet test --collect:"XPlat Code Coverage"
3. Generate the coverage report (if using Coverlet and ReportGenerator):
    ```bash
    reportgenerator -reports:TestResults/**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html

## FeedBack on Tasks

#### Task: Define Models
**Was it easy to complete the task using AI?**
* Yes, defining models was straightforward with AI assistance. The guidance provided clear steps to create the necessary classes with the required properties.

**How long did the task take you to complete?**
* Approximately 30 minutes.

**Was the code ready to run after generation? What did you have to change to make it usable?**
* The code was mostly ready. However, I needed to add data annotations and relationships between models, such as navigation properties, to make it fully functional.

**Which challenges did you face during the completion of the task?**
* Ensuring that the models were correctly set up with data annotations and relationships was slightly challenging.

**Which specific prompts you learned as a good practice to complete the task?**
* Requesting clear definitions of model properties and their data types helped ensure completeness. Asking for guidance on relationships between models was useful.

#### Task: Data Access Layer (DAL)
**Was it easy to complete the task using AI?**
* Yes, AI provided a solid foundation for setting up the DbContext, repository interfaces, and implementations.

**How long did the task take you to complete?**
* Approximately 45 minutes.

**Was the code ready to run after generation? What did you have to change to make it usable?**
* The generated code was mostly functional. I had to configure the DbContext and connection string, and ensure that migrations were correctly applied.

**Which challenges did you face during the completion of the task?**
* Configuring the connection string and ensuring the database schema was up-to-date were the main challenges.

**Which specific prompts you learned as a good practice to complete the task?**
* Asking for instructions on setting up and configuring DbContext and migrations proved to be very helpful.

#### Task: Business Layer (BL)
**Was it easy to complete the task using AI?**
* Yes, the AI provided clear steps for creating service interfaces and their implementations, along with business logic.

**How long did the task take you to complete?**
* Approximately 40 minutes.

**Was the code ready to run after generation? What did you have to change to make it usable?**
* The code was ready to run after minor adjustments. I had to ensure that the services correctly interacted with the repositories and handled exceptions appropriately.

**Which challenges did you face during the completion of the task?**
* Ensuring that business logic was correctly implemented and integrated with the repository layer was a bit challenging.

**Which specific prompts you learned as a good practice to complete the task?**
* Requesting detailed implementation examples for service methods and error handling was particularly useful.

#### Task: Controller Layer
**Was it easy to complete the task using AI?**
* Yes, AI provided a good starting point for creating the API controllers and implementing CRUD operations.

**How long did the task take you to complete?**
* Approximately 1 hour.

**Was the code ready to run after generation? What did you have to change to make it usable?**
* The code was mostly ready. I needed to ensure that the routing and dependency injection were correctly configured.

**Which challenges did you face during the completion of the task?**
* Correctly implementing error handling and ensuring that all endpoints functioned as expected were the main challenges.

**Which specific prompts you learned as a good practice to complete the task?**
* Asking for detailed examples of CRUD operations and error handling in API controllers helped streamline the process.

#### Task: Integrate Third-Party Services
**Was it easy to complete the task using AI?**
* Integrating third-party services was manageable with AI guidance. It provided clear instructions on how to call external APIs.

**How long did the task take you to complete?**
* Approximately 1 hour.

**Was the code ready to run after generation? What did you have to change to make it usable?**
* The code was mostly functional, but I had to handle API key management and ensure that the third-party service integration was correctly configured.

**Which challenges did you face during the completion of the task?**
* Handling API responses and managing configuration settings for the third-party service were challenging.

**Which specific prompts you learned as a good practice to complete the task?**
* Asking for clear instructions on API integration and error handling in service classes was highly beneficial.

#### Task: Testing
**Was it easy to complete the task using AI?**
* Testing with AI assistance was straightforward. It provided clear examples of how to set up unit tests and use an in-memory database.

#### How long did the task take you to complete?
* Approximately 1.5 hours.

**Was the code ready to run after generation? What did you have to change to make it usable?**
* The generated test code was mostly ready to run, but I had to adjust configurations to ensure isolation of tests and handle any specific test cases.

**Which challenges did you face during the completion of the task?**
* Ensuring that tests were independent and correctly set up to use an in-memory database was a key challenge.

**Which specific prompts you learned as a good practice to complete the task?**
* Asking for examples of isolated unit tests and guidance on setting up in-memory databases was highly beneficial.