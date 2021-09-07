using API.Domain.Business.Interface;
using API.Domain.Models;
using API.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Business
{
    public class ChargeStationBusiness : IChargeStationBusiness
    {
        private readonly IChargeStationRepository _chargeStationRepository;

        public ChargeStationBusiness(IChargeStationRepository chargeStationRepository)
        {
            _chargeStationRepository = chargeStationRepository;
        }

        public Task<ChargeStationModel> GetChargeStationById(Guid id) =>
            _chargeStationRepository.GetChargeStationByIdAsync(id);

        public Task<List<ChargeStationModel>> GetChargeStationsByGroup(Guid groupId) =>
            _chargeStationRepository.GetAllChargeStationsByGroupIdAsync(groupId);
    }
}