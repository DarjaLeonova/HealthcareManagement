using HealthcareManagement.Business.Services.Interfaces;
using HealthcareManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagement.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IRouteService _routeService;


    public PatientsController(IPatientService patientService, IRouteService routeService)
    {
        _patientService = patientService;
        _routeService = routeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult> AddPatient(Patient patient)
    {
        await _patientService.AddPatientAsync(patient);
        return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePatient(int id, Patient patient)
    {
        if (id != patient.Id)
        {
            return BadRequest();
        }

        await _patientService.UpdatePatientAsync(patient);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePatient(int id)
    {
        await _patientService.DeletePatientAsync(id);
        return NoContent();
    }
    
    [HttpGet("directions")]
    public async Task<ActionResult> GetDirections(string origin, string destination)
    {
        var directions = await _routeService.GetDirectionsAsync(origin, destination);
        return Ok(directions);
    }
}