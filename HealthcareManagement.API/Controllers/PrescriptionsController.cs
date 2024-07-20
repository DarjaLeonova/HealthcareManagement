using HealthcareManagement.Business.Services.Interfaces;
using HealthcareManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions()
    {
        var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
        return Ok(prescriptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Prescription>> GetPrescription(int id)
    {
        var prescription = await _prescriptionService.GetPrescriptionByIdAsync(id);
        if (prescription == null)
        {
            return NotFound();
        }
        return Ok(prescription);
    }

    [HttpPost]
    public async Task<ActionResult> AddPrescription(Prescription prescription)
    {
        await _prescriptionService.AddPrescriptionAsync(prescription);
        return CreatedAtAction(nameof(GetPrescription), new { id = prescription.Id }, prescription);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrescription(int id, Prescription prescription)
    {
        if (id != prescription.Id)
        {
            return BadRequest();
        }

        await _prescriptionService.UpdatePrescriptionAsync(prescription);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePrescription(int id)
    {
        await _prescriptionService.DeletePrescriptionAsync(id);
        return NoContent();
    }
}