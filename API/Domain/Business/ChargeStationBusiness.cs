using API.Domain.Business.Interface;
using API.Domain.Exceptions;
using API.Domain.Models;
using API.Domain.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.Business
{
    public class ChargeStationBusiness : IChargeStationBusiness
    {
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly IGroupRepository _groupRepository;

        public ChargeStationBusiness(IChargeStationRepository chargeStationRepository, IGroupRepository groupRepository)
        {
            _chargeStationRepository = chargeStationRepository;
            _groupRepository = groupRepository;
        }

        public async Task<ChargeStationModel> Create(ChargeStationModel model)
        {
            var group = await _groupRepository.GetById(model.GroupId);

            if (model.Connectors.Count >= 5)
            {
                throw new BusinessException("You can't add more than 5 connectors to this charge station.");
            }

            if (model.GroupId == Guid.Empty)
            {
                throw new BusinessException("The Charge Station cannot exist in the domain without Group.");
            }

            if (group != null && group.Id != model.GroupId)
            {
                throw new BusinessException("The Charge Station can be only in one Group at the same time.");
            }

            if (model.Connectors == null || !model.Connectors.Any())
            {
                throw new BusinessException("You can't create a charge station without a connector");
            }
            var chargeStationCreated = await _chargeStationRepository.Create(model);
            return chargeStationCreated;
        }

        public async Task<ChargeStationModel> Update(ChargeStationModel model)
        {
            if (model.Connectors.Count >= 5)
            {
                throw new BusinessException("You can't add more than 5 connectors to this charge station.");
            }

            var chargeStationUpdated = await _chargeStationRepository.Update(model);

            return chargeStationUpdated;
        }

        public async Task Remove(Guid chargeStationId)
        {
            try
            {
                _chargeStationRepository.Remove(chargeStationId);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw;
                }
            }
        }

        public Task<ChargeStationModel> GetById(Guid id) => _chargeStationRepository.GetByIdAsync(id);

        public Task<List<ChargeStationModel>> GetChargeStationsByGroupId(Guid groupId) => _chargeStationRepository.GetAllChargeStationsByGroupIdAsync(groupId);
    }
}