using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using GreenFlux.Domain.Business.Interface;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Domain.Models;
using GreenFlux.Domain.Repository;

namespace GreenFlux.Domain.Business
{
    public class GroupBusiness : IGroupBusiness
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IChargeStationRepository _chargeStationRepository;

        public GroupBusiness(IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository)
        {
            _groupRepository = groupRepository;
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
            var chargeStationsExist = await _chargeStationRepository.GetAllChargeStationsByGroupIdAsync(groupModel.Id);
            if (chargeStationsExist != null && chargeStationsExist.Any())
            {
                throw new BusinessException("The Charge Station can be only in one Group at the same time.");
            }

            if (groupModel.ChargeStations != null && groupModel.ChargeStations.Any())
            {
                if (groupModel.ChargeStations.Count > 1)
                {
                    throw new BusinessException("You can only create one Charge Station per call.");
                }

                decimal sum1 = 0;
                List<ConnectorModel> listConnectorToBeRemoved = new List<ConnectorModel>();
                List<ConnectorModel> connectors = new List<ConnectorModel>();

                foreach (var chargeStation in groupModel.ChargeStations)
                {
                    connectors = chargeStation.Connectors.ToList();
                    if (connectors.Count > 5)
                    {
                        throw new BusinessException("You can't add more than 5 connectors to this charge station.");
                    }
                    foreach (var connector in connectors)
                    {
                        sum1 += connector.MaxCurrent;

                        if (sum1 > groupModel.Capacity)
                        {
                            listConnectorToBeRemoved.Add(connector);
                        }
                    }
                }

                connectors.RemoveAll(item => listConnectorToBeRemoved.Contains(item));

                if (connectors.Any())
                {
                    groupModel.ChargeStations.First().Connectors = connectors;
                }
            }

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
                    throw;
                }
            }
        }

        public async Task<GroupModel> Update(GroupModel model)
        {
            try
            {
                var chargeStationsExist = await _chargeStationRepository.GetAllChargeStationsByGroupIdAsync(model.Id);
                if (chargeStationsExist != null && chargeStationsExist.Any())
                {
                    throw new BusinessException("The Charge Station can be only in one Group at the same time.");
                }

                if (model.ChargeStations != null && model.ChargeStations.Any())
                {
                    if (model.ChargeStations.Count > 1)
                    {
                        throw new BusinessException("You can only create one Charge Station per call.");
                    }

                    decimal sum1 = 0;
                    List<ConnectorModel> listConnectorToBeRemoved = new List<ConnectorModel>();
                    List<ConnectorModel> connectors = new List<ConnectorModel>();

                    foreach (var chargeStation in model.ChargeStations)
                    {
                        connectors = chargeStation.Connectors.ToList();
                        if (connectors.Count > 5)
                        {
                            throw new BusinessException("You can't add more than 5 connectors to this charge station.");
                        }
                        foreach (var connector in connectors)
                        {
                            sum1 += connector.MaxCurrent;

                            if (sum1 > model.Capacity)
                            {
                                listConnectorToBeRemoved.Add(connector);
                            }
                        }
                    }

                    connectors.RemoveAll(item => listConnectorToBeRemoved.Contains(item));

                    if (connectors.Any())
                    {
                        model.ChargeStations.First().Connectors = connectors;
                    }

                    var capacityConnectorsOfAllChargeStations = model.ChargeStations
                        .SelectMany(x => x.Connectors).Sum(x => x.MaxCurrent);

                    if (model.Capacity < capacityConnectorsOfAllChargeStations)
                    {
                        throw new BusinessException("A group's capacity must be greater or equal to the combined capacity of its members.");
                    }
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