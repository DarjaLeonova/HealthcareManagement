namespace HealthcareManagement.Business.Services.Interfaces;

public interface IRouteService
{
    Task<string> GetDirectionsAsync(string origin, string destination);
}