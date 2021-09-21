using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Presentation.DTO;
using Newtonsoft.Json;
using Xunit;

namespace GreenFlux.IntegrationTest.Controllers
{
    public class GroupControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GroupControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetGroup_GivenGroupId_ReturnsOk()
        {
            //arrange
            Guid groupId = new Guid("d9967e27-8dd6-42d5-99c2-a820f309afaf");

            //act
            var response = await _client.GetAsync(ApiRoutes.Group.Get.Replace("{groupId}", groupId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Id.Should().Be(groupId);
        }

        [Fact]
        public async Task GetGroup_GivenGroupGuidEmpty_ReturnsNotFound()
        {
            //arrange
            Guid groupId = Guid.Empty;

            //act
            var response = await _client.GetAsync(ApiRoutes.Group.Get.Replace("{groupId}", groupId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Id.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateGroup_GivenGroup_ReturnsOk()
        {
            //arrange
            var groupDto = new GroupDTO
            {
                Id = Guid.NewGuid(),
                Name = "Group 0030",
                Capacity = 200
            };
            var serializeObject = JsonConvert.SerializeObject(groupDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PostAsync(ApiRoutes.Group.Post, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Id.Should().Be(groupDto.Id);
        }

        [Fact]
        public async Task CreateGroup_GivenGroupAndChargeWithConnectorsCapacityOverLimitInGroup_ReturnsBadRequest()
        {
            //arrange
            //arrange
            var groupId = new Guid("7C893936-1992-46D3-9551-90AEE081C463");

            var groupDto = new GroupDTO
            {
                Id = groupId,
                Name = "Update IT Group",
                Capacity = 1000
            };

            var chargeStation = new ChargeStationDTO
            {
                GroupId = groupId,
                Name = "Charge Station IT",
                Id = Guid.NewGuid()
            };
            var listChargeStation = new List<ChargeStationDTO>();
            listChargeStation.Add(chargeStation);

            List<ConnectorDTO> listConnector = new List<ConnectorDTO>();
            for (int i = 0; i < 2; i++)
            {
                var connector = new ConnectorDTO
                {
                    Id = Guid.NewGuid(),
                    MaxCurrent = 500 + i,
                    ChargeStationId = chargeStation.Id
                };
                listConnector.Add(connector);
            }

            groupDto.ChargeStations = null;
            groupDto.ChargeStations = listChargeStation;

            groupDto.ChargeStations.First().Connectors = listConnector;

            var serializeObject = JsonConvert.SerializeObject(groupDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PostAsync(ApiRoutes.Group.Post, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteGroup_GivenGroupId_ReturnsOk()
        {
            //arrange

            var groupId = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4");

            //act
            var response = await _client.DeleteAsync(ApiRoutes.Group.Delete.Replace("{groupId}", groupId.ToString()));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Should().BeNull();
        }

        [Fact]
        public async Task UpdateGroup_GivenGroup_ReturnsOk()
        {
            //arrange
            var groupId = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4");

            var groupDto = new GroupDTO
            {
                Id = groupId,
                Name = "Update IT Group",
                Capacity = 9000
            };

            var serializeObject = JsonConvert.SerializeObject(groupDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PutAsync(ApiRoutes.Group.Put, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Name.Should().Be(groupDto.Name);
            group.Capacity.Should().Be(groupDto.Capacity);
        }

        [Fact]
        public async Task UpdateGroup_GivenGroupAndChargeWithConnectorsCapacityOverLimitInGroup_ReturnsBadRequest()
        {
            //arrange
            var groupId = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4");

            var groupDto = new GroupDTO
            {
                Id = groupId,
                Name = "Update IT Group",
                Capacity = 1000
            };

            var chargeStation = new ChargeStationDTO
            {
                GroupId = groupId,
                Name = "Charge Station IT",
                Id = Guid.NewGuid()
            };
            var listChargeStation = new List<ChargeStationDTO>();
            listChargeStation.Add(chargeStation);

            List<ConnectorDTO> listConnector = new List<ConnectorDTO>();
            for (int i = 0; i < 2; i++)
            {
                var connector = new ConnectorDTO
                {
                    Id = Guid.NewGuid(),
                    MaxCurrent = 500 + i,
                    ChargeStationId = chargeStation.Id
                };
                listConnector.Add(connector);
            }

            groupDto.ChargeStations = null;
            groupDto.ChargeStations = listChargeStation;

            groupDto.ChargeStations.First().Connectors = listConnector;

            var serializeObject = JsonConvert.SerializeObject(groupDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PutAsync(ApiRoutes.Group.Put, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateGroup_GivenGroupWithGroupIdNull_ReturnsNotFound()
        {
            //arrange
            var groupId = Guid.Empty;

            var groupDto = new GroupDTO
            {
                Id = groupId,
                Name = "Update IT Group",
                Capacity = 9000
            };

            var serializeObject = JsonConvert.SerializeObject(groupDto);
            var stringContent = new StringContent(serializeObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            //act
            var response = await _client.PutAsync(ApiRoutes.Group.Put, stringContent);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var group = JsonConvert.DeserializeObject<GroupDTO>(stringResponse);
            group.Id.Should().BeEmpty();
        }
    }
}