using FluentAssertions;
using Newtonsoft.Json;
using CodingAssessment.Domain.Entities;
using System.Net;

namespace WebApi.EndToEndTests.Controllers
{
    [Collection("DatabaseCollectionFixture")]
    public class FoodAndDrugServiceTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public FoodAndDrugServiceTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

        }

        [Fact(DisplayName = "Check_FoodAndDrugAdministration_Success")]
        public async Task Check_FoodAndDrugAdministration_Success()
        {
            // Arrange
            var path = "FoodAndDrugAdministration/Foodenforcement";

            // Act
            var responseMessage = await _client.GetAsync(path);
            var content = await responseMessage.Content.ReadAsStringAsync();

            var meta = JsonConvert.DeserializeObject<Meta>(content);

            // Assert
            meta.Should().BeOfType<Meta>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Check_FoodAndDrugAdministration_EmailSend_Success")]
        public async Task Check_FoodAndDrugAdministration_EmailSend_Success()
        {
            // Arrange
            var path = "FoodAndDrugAdministration/SendEmail";

            // Act
            var responseMessage = await _client.GetAsync(path);
            var content = await responseMessage.Content.ReadAsStringAsync();

            var meta = JsonConvert.DeserializeObject<Meta>(content);

            // Assert
            meta.Should().BeOfType<Meta>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
