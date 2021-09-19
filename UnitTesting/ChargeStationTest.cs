using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Business;
using API.Domain.Business.Interface;
using API.Domain.Exceptions;
using API.Domain.Models;
using API.Domain.Repository;
using AutoFixture.Xunit2;
using NSubstitute;
using Xunit;

namespace UnitTesting
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
        public async Task GetChargeStation_GivenChargeStationId_ReturnsTheChargeStation(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
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
        public async Task CreateChargeStation_GivenChargeStation_ReturnsTheChargeStation(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
        {
            //arrange
            chargeStationModel.Name = "ChargeStation 00020";
            _chargeStationRepository.WhenForAnyArgs(g => g.Create(chargeStationModel)).Do(g => { chargeStationModel.Id = chargeStationGuid;
            });

            //act
            await _chargeStationBusiness.Create(chargeStationModel);

            //assert
            await _chargeStationRepository.Received().Create(chargeStationModel);
            Assert.Equal(chargeStationGuid, chargeStationModel.Id);
        }

        [Theory, AutoData]
        public async Task CreateChargeStation_GivenChargeStationWithGroupNull_ThrowsException(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
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
        public async Task CreateChargeStation_GivenChargeStationWithNoConnector_ThrowsException(ChargeStationModel chargeStationModel, Guid chargeStationGuid)
        {
            //arrange
            chargeStationModel.Name = "Charge Station 0030";
            chargeStationModel.Connectors = null;
            _chargeStationRepository.WhenForAnyArgs(c => c.Create(chargeStationModel))
                .Do(c => { chargeStationModel.Id = chargeStationGuid; });

            //act
            var ex = await Assert.ThrowsAsync<BusinessException>(() => _chargeStationBusiness.Create(chargeStationModel));

            //assert
            Assert.Equal("The Charge Station cannot exist in the domain without Group.", ex.Message);
        }
    }
}
