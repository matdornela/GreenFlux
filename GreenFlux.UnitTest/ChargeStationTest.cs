﻿using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using GreenFlux.Domain.Business;
using GreenFlux.Domain.Business.Interface;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Domain.Models;
using GreenFlux.Domain.Repository;
using NSubstitute;
using Xunit;

namespace GreenFlux.UnitTest
{
    public class ChargeStationTest
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly IChargeStationBusiness _chargeStationBusiness;

        public ChargeStationTest()
        {
            _groupRepository = Substitute.For<IGroupRepository>();
            _chargeStationRepository = Substitute.For<IChargeStationRepository>();
            _chargeStationBusiness = new ChargeStationBusiness(_chargeStationRepository, _groupRepository);
        }

        [Theory, AutoData]
        public async Task GetById_GivenChargeStationId_ReturnsChargeStation(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
        {
            //arrange
            _chargeStationRepository.GetByIdAsync(chargeStationGuid).Returns(chargeStationModel);

            //act
            var chargeStation = await _chargeStationBusiness.GetById(chargeStationGuid);

            //assert
            await _chargeStationRepository.Received().GetByIdAsync(chargeStationGuid);
            Assert.Equal(chargeStationModel.Id, chargeStation.Id);
        }

        [Theory, AutoData]
        public async Task Create_GivenChargeStation_ReturnsChargeStation(ChargeStationModel chargeStationModel)
        {
            //arrange
            chargeStationModel.Name = "ChargeStation 00020";
            chargeStationModel.Id = Guid.NewGuid();
            _chargeStationRepository.Create(chargeStationModel).Returns(chargeStationModel);

            //act
            var chargeStationCreated = await _chargeStationBusiness.Create(chargeStationModel);

            //assert
            await _chargeStationRepository.Received().Create(chargeStationModel);
            Assert.Equal(chargeStationModel.Id, chargeStationCreated.Id);
        }

        [Theory, AutoData]
        public async Task Create_GivenChargeStationWithGroupNull_ThrowsException(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
        {
            //arrange
            chargeStationModel.Name = "Charge Station 0030";
            chargeStationModel.GroupId = Guid.Empty;
            _chargeStationRepository.WhenForAnyArgs(c => c.Create(chargeStationModel))
                .Do(c => { chargeStationModel.Id = chargeStationGuid; });

            //act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _chargeStationBusiness.Create(chargeStationModel));

            //assert
            Assert.Equal("The Charge Station cannot exist in the domain without Group.", ex.Message);
        }

        [Theory, AutoData]
        public async Task Create_GivenChargeStationWithNoConnector_ThrowsException(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
        {
            //arrange
            chargeStationModel.Name = "Charge Station 0030";
            chargeStationModel.Connectors = null;
            _chargeStationRepository.WhenForAnyArgs(c => c.Create(chargeStationModel))
                .Do(c => { chargeStationModel.Id = chargeStationGuid; });

            //act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _chargeStationBusiness.Create(chargeStationModel));

            //assert
            Assert.Equal("You can't create a charge station without a connector.", ex.Message);
        }
    }
}