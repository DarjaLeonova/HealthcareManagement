using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HealthcareManagement.Business.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace HealthcareManagement.Tests.Services
{
    public class RouteServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHandler;
        private readonly HttpClient _httpClient;
        private readonly RouteService _routeService;
        private readonly string _apiKey = "fake_api_key";

        public RouteServiceTests()
        {
            _mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_mockHandler.Object);
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c["OpenRouteService:ApiKey"]).Returns(_apiKey);

            _routeService = new RouteService(_httpClient, configuration.Object);
        }

        [Fact]
        public async Task GetDirectionsAsync_ReturnsDirections()
        {
            // Arrange
            var origin = "8.681495,49.41461";
            var destination = "8.687872,49.420318";
            var jsonResponse = new JObject
            {
                ["routes"] = new JArray(
                    new JObject
                    {
                        ["summary"] = new JObject
                        {
                            ["duration"] = 600
                        }
                    })
            }.ToString();

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new Uri($"https://api.openrouteservice.org/v2/directions/driving-car?api_key={_apiKey}&start={origin}&end={destination}")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse),
                })
                .Verifiable();

            // Act
            var directions = await _routeService.GetDirectionsAsync(origin, destination);

            // Assert
            Assert.Contains("routes", directions);
            _mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri == new Uri($"https://api.openrouteservice.org/v2/directions/driving-car?api_key={_apiKey}&start={origin}&end={destination}")),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetDirectionsAsync_ThrowsException_OnError()
        {
            // Arrange
            var origin = "8.681495,49.41461";
            var destination = "8.687872,49.420318";

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new Uri($"https://api.openrouteservice.org/v2/directions/driving-car?api_key={_apiKey}&start={origin}&end={destination}")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                })
                .Verifiable();

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _routeService.GetDirectionsAsync(origin, destination));
        }
    }
}