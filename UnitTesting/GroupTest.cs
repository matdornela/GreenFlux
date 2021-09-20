using AutoFixture.Xunit2;
using GreenFlux.Domain.Business;
using GreenFlux.Domain.Business.Interface;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Domain.Models;
using GreenFlux.Domain.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{
    public class GroupTest
    {
        private readonly IGroupBusiness _groupBusiness;
        private readonly IGroupRepository _groupRepository;
        private readonly IChargeStationRepository _chargeStationRepository;

        public GroupTest()
        {
            _groupRepository = Substitute.For<IGroupRepository>();
            _chargeStationRepository = Substitute.For<IChargeStationRepository>();
            _groupBusiness = new GroupBusiness(_groupRepository, _chargeStationRepository);
        }

        [Theory, AutoData]
        public async Task GetById_GivenGroupId_ReturnsGroup(GroupModel groupModel, Guid groupGuid)
        {
            //arrange
            _groupRepository.GetById(groupGuid).Returns(groupModel);

            //act
            var group = await _groupBusiness.GetById(groupGuid);

            //assert
            await _groupRepository.Received().GetById(groupGuid);
            Assert.Equal(groupModel.Id, group.Id);
        }

        [Theory, AutoData]
        public async Task Create_GivenGroup_ReturnsGroup(GroupModel groupModel, Guid groupGuid)
        {
            //arrange
            groupModel.Name = "Group 00020";
            groupModel.Capacity = 20;
            groupModel.ChargeStations = null;
            _groupRepository.WhenForAnyArgs(g => g.Create(groupModel)).Do(g => { groupModel.Id = groupGuid; });

            //act
            var group = await _groupBusiness.Create(groupModel);

            //assert
            await _groupRepository.Received().Create(groupModel);
            Assert.Equal(groupGuid, groupModel.Id);
        }

        [Theory, AutoData]
        public async Task Create_GivenGroupWithMoreThanOneChargeStation_ThrowsException(GroupModel groupModel, Guid groupGuid)
        {
            //arrange
            groupModel.Name = "Group 00021";
            groupModel.Capacity = 21;
            _groupRepository.WhenForAnyArgs(g => g.Create(groupModel)).Do(g => { groupModel.Id = groupGuid; });

            //act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _groupBusiness.Create(groupModel));

            //assert
            Assert.Equal("You can only create one Charge Station per call.", ex.Message);
        }

        [Theory, AutoData]
        public async Task Create_GivenGroupAndChargeStationWithMoreThanFiveConnectors_ThrowsException(GroupModel groupModel, Guid groupGuid)
        {
            //arrange
            groupModel.Name = "Group 00022";
            groupModel.Capacity = 22;
            groupModel.ChargeStations = null;

            var chargeStationModel = new ChargeStationModel();
            chargeStationModel.GroupId = groupGuid;
            chargeStationModel.Id = Guid.NewGuid();
            chargeStationModel.Name = "Charge Station 0022";

            _groupRepository.WhenForAnyArgs(g => g.Create(groupModel)).Do(g => { groupModel.Id = groupGuid; });

            var listChargeStation = new List<ChargeStationModel>();
            listChargeStation.Add(chargeStationModel);

            groupModel.ChargeStations = listChargeStation;

            List<ConnectorModel> connectors = new List<ConnectorModel>();
            for (int i = 0; i < 6; i++)
            {
                var connectorModel = new ConnectorModel
                {
                    Id = Guid.NewGuid(),
                    MaxCurrent = 5,
                    ChargeStationId = groupModel.ChargeStations.First().Id
                };
                connectors.Add(connectorModel);
            }
            groupModel.ChargeStations.First().Connectors = connectors;

            //act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _groupBusiness.Create(groupModel));

            //assert
            Assert.Equal("You can't add more than 5 connectors to this charge station.", ex.Message);
        }
    }
}