using AutoFixture.Xunit2;
using GreenFlux.Domain.Business;
using GreenFlux.Domain.Business.Interface;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Domain.Models;
using GreenFlux.Domain.Repository;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{
    public class ConnectorTest
    {
        private readonly IConnectorRepository _connectorRepository;
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly IConnectorBusiness _connectorBusiness;

        public ConnectorTest()
        {
            _connectorRepository = Substitute.For<IConnectorRepository>();
            _chargeStationRepository = Substitute.For<IChargeStationRepository>();
            _connectorBusiness = new ConnectorBusiness(_connectorRepository, _chargeStationRepository);
        }

        [Theory, AutoData]
        public async Task GetConnector_GivenConnectorId_ReturnsTheConnector(ConnectorModel connectorModel, Guid groupGuid)
        {
            //arrange
            _connectorRepository.GetById(groupGuid).Returns(connectorModel);

            //act
            var connector = await _connectorBusiness.GetById(groupGuid);

            //assert
            await _connectorRepository.Received().GetById(groupGuid);
            Assert.Equal(connector.Id, connector.Id);
        }

        [Theory, AutoData]
        public async Task CreateConnector_GivenConnector_ReturnsTheConnector(ConnectorModel connectorModel, Guid connectorGuid)
        {
            Guid g1 = Guid.NewGuid();
            //arrange
            connectorModel.ChargeStationId = g1;
            connectorModel.MaxCurrent = 5;
            _connectorRepository.WhenForAnyArgs(g => g.Create(connectorModel)).Do(g => { connectorModel.Id = connectorGuid; });

            //act
            await _connectorBusiness.Create(connectorModel);

            //assert
            await _connectorRepository.Received().Create(connectorModel);
            Assert.Equal(connectorGuid, connectorModel.Id);
        }

        [Theory, AutoData]
        public async Task CreateConnector_GivenConnectorWithChargeStationNull_ThrowsException(ConnectorModel connectorModel, Guid connectorGuid)
        {
            //arrange
            connectorModel.MaxCurrent = 20;
            connectorModel.ChargeStationId = Guid.Empty;
            _connectorRepository.WhenForAnyArgs(c => c.Create(connectorModel))
                .Do(c => { connectorModel.Id = connectorGuid; });

            //act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _connectorBusiness.Create(connectorModel));

            //assert
            Assert.Equal("A Connector cannot exist in the domain without a Charge Station.", ex.Message);
        }
    }
}