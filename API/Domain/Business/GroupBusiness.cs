using API.Domain.Business.Interface;
using API.Domain.Exceptions;
using API.Domain.Models;
using API.Domain.Repository;
using API.Presentation.Exceptions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace API.Domain.Business
{
    public class GroupBusiness : IGroupBusiness
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly IMapper _mapper;

        public GroupBusiness(IGroupRepository groupRepository, IMapper mapper, IChargeStationRepository chargeStationRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _chargeStationRepository = chargeStationRepository;
        }

        public Task<List<GroupModel>> GetAll()
        => _groupRepository.GetAll();

        public async Task<GroupModel> GetById(Guid id)
        {
            var data = await _groupRepository.GetById(id);
            return data;
        }

        public async Task<GroupModel> Create(GroupModel groupModel)
        {
            if (groupModel.ChargeStations.Count > 1)
            {
                throw new BusinessException("You can only create one Charge Station per call.");
            }

            var chargeStations = groupModel.ChargeStations.ToList();

            var chargeStationConnectors = chargeStations.SelectMany(x => x.Connectors).ToList();

            List<ConnectorModel> listConnector = new List<ConnectorModel>();

            decimal sum = 0;

            foreach (var connectorModel in chargeStationConnectors)
            {
                sum += connectorModel.MaxCurrent;

                if (sum > groupModel.Capacity)
                {
                    listConnector.Add(connectorModel);
                }
            }

            chargeStationConnectors.RemoveAll(item => listConnector.Contains(item));

            groupModel.ChargeStations.First().Connectors = chargeStationConnectors;

            var groupCreated = await _groupRepository.Create(groupModel);

            return groupCreated;
        }

        public async Task Remove(Guid groupId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var group = await _groupRepository.GetById(groupId);

                    var listChargeStation = group.ChargeStations.ToList();

                    foreach (var chargeStation in listChargeStation)
                    {
                        _chargeStationRepository.Remove(chargeStation.Id);
                    }

                    _groupRepository.Delete(groupId);
                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw new NotFoundException(e.Message);
                }
            }
        }

        public async Task<GroupModel> Update(GroupModel model)
        {
            try
            {
                var group = await _groupRepository.GetById(model.Id);

                var listChargeStation = group.ChargeStations.ToList();

                var capacityConnectorsOfAllChargeStations = listChargeStation.SelectMany(x => x.Connectors).Sum(x => x.MaxCurrent);

                if (model.Capacity < capacityConnectorsOfAllChargeStations)
                {
                    throw new BusinessException("A group's capacity must be greater or equal to the combined capacity of its members.");
                }
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw;
                }
            }

            var groupUpdated = await _groupRepository.Update(model);
            return groupUpdated;
        }
    }
}