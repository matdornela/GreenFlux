using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using API;
using API.Presentation.DTO;
using API.Routes;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTesting.Controllers
{
    public class ConnectorControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ConnectorControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetConnector_GivenConnectorId_ReturnsOk()
        {
            //arrange
            Guid connectorId = new Guid("0bc574a3-0184-4ecb-b3b9-81119af4aba7");

            //act
            var response = await _client.GetAsync(ApiRoutes.Connector.Get.Replace("{connectorId}", connectorId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<ConnectorDTO>(stringResponse);
            group.Id.Should().Be(connectorId);
        }

        [Fact]
        public async Task GetConnector_GivenConnectorGuidEmpty_ReturnsNotFound()
        {
            //arrange
            Guid connectorId = Guid.Empty;

            //act
            var response = await _client.GetAsync(ApiRoutes.Connector.Get.Replace("{connectorId}", connectorId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateConnector_GivenConnector_ReturnsOk()
        {
            Guid chargeStationGuid = Guid.NewGuid();

            //arrange
            var connectorDto = new ConnectorDTO()
            {
                Id = Guid.NewGuid(),
                ChargeStationId = chargeStationGuid,
                MaxCurrent = 100
            };

            var serializeObject = JsonConvert.SerializeObject(connectorDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PostAsync(ApiRoutes.Group.Post, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var connector = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            connector.Id.Should().Be(connectorDto.Id);
        }

        [Fact]
        public async Task DeleteConnector_GivenConnectorId_ReturnsOk()
        {
            //arrange

            var connectorId = new Guid("0bc574a3-0184-4ecb-b3b9-81119af4aba7");

            //act
            var response = await _client.DeleteAsync(ApiRoutes.Connector.Delete.Replace("{connectorId}", connectorId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Should().BeNull();
        }

        [Fact]
        public async Task UpdateConnector_GivenConnector_ReturnsOk()
        {
            //arrange
            var connectorId = new Guid("0bc574a3-0184-4ecb-b3b9-81119af4aba7");

            var chargeStationId = new Guid("5451119c-28af-41ba-a8f6-db43a4072edc");

            var connectorDto = new ConnectorDTO()
            {
                Id = connectorId,
                ChargeStationId = chargeStationId,
                MaxCurrent = 10000
            };

            var serializeObject = JsonConvert.SerializeObject(connectorDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PutAsync(ApiRoutes.Connector.Put, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var connector = JsonConvert.DeserializeObject<ConnectorDTO>(stringResponse);
            connector.Id.Should().Be(connectorDto.Id);
        }
    }
}