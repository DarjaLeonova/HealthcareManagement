using HealthcareManagement.Business.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace HealthcareManagement.Business.Services;

public class RouteService : IRouteService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RouteService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenRouteService:ApiKey"];
    }

    public async Task<string> GetDirectionsAsync(string origin, string destination)
    {
        var requestUri = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={_apiKey}&start={origin}&end={destination}";
        var response = await _httpClient.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Failed to fetch directions from OpenRouteService API.");
        }

        var json = await response.Content.ReadAsStringAsync();
        var directions = JObject.Parse(json);

        return directions.ToString();
    }
}