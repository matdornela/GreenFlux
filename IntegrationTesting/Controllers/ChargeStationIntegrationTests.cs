using API;
using API.Presentation.DTO;
using API.Routes;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTesting.Controllers
{
    public class ChargeStationIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ChargeStationIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetChargeStation_GivenChargeStationId_ReturnsOk()
        {
            //arrange
            Guid chargeStationId = new Guid("5451119c-28af-41ba-a8f6-db43a4072edc");

            //act
            var response = await _client.GetAsync(ApiRoutes.ChargeStation.Get.Replace("{chargeStationId}", chargeStationId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var chargeStation = JsonConvert.DeserializeObject<ChargeStationDTO>(stringResponse);
            chargeStation.Id.Should().Be(chargeStationId);
        }

        [Fact]
        public async Task GetChargeStation_GivenChargeStationGuidEmpty_ReturnsNotFound()
        {
            //arrange
            Guid chargeStationId = Guid.Empty;

            //act
            var response = await _client.GetAsync(ApiRoutes.ChargeStation.Get.Replace("{chargeStationId}", chargeStationId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateChargeStation_GivenChargeStationWithNullConnector_ReturnsBadRequest()
        {
            //arrange
            var chargeStationDto = new ChargeStationDTO
            {
                Id = Guid.NewGuid(),
                Name = "ChargeStation 0030",
                GroupId = new Guid("d9967e27-8dd6-42d5-99c2-a820f309afaf")
            };
            chargeStationDto.Connectors = null;

            var serializeObject = JsonConvert.SerializeObject(chargeStationDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PostAsync(ApiRoutes.ChargeStation.Post, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteChargeStation_GivenChargeStationId_ReturnsOk()
        {
            //arrange

            var chargeStationId = new Guid("5451119c-28af-41ba-a8f6-db43a4072edc");

            //act
            var response = await _client.DeleteAsync(ApiRoutes.ChargeStation.Delete.Replace("{chargeStationId}", chargeStationId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var chargeStation = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            chargeStation.Should().BeNull();
        }

        [Fact]
        public async Task UpdateChargeStation_GivenChargeStation_ReturnsOk()
        {
            //arrange
            Guid chargeStationId = new Guid("5451119c-28af-41ba-a8f6-db43a4072edc");
            var chargeStationDto = new ChargeStationDTO
            {
                Id = chargeStationId,
                Name = "ChargeStation 0031",
                GroupId = new Guid("d9967e27-8dd6-42d5-99c2-a820f309afaf"),
                Connectors = null
            };

            var serializeObject = JsonConvert.SerializeObject(chargeStationDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PutAsync(ApiRoutes.ChargeStation.Put, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var chargeStation = JsonConvert.DeserializeObject<ChargeStationDTO>(stringResponse);
            chargeStation.Name.Should().Be(chargeStationDto.Name);
            chargeStation.Id.Should().Be(chargeStationDto.Id);
        }
    }
}