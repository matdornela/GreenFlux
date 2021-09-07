using API.Domain.Business.Interface;
using API.Domain.Models;
using API.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.Business
{
    public class ConnectorBusiness : IConnectorBusiness
    {
        private readonly IConnectorRepository _connectorRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IChargeStationRepository _chargeStationRepository;

        public ConnectorBusiness(IConnectorRepository connectorRepository, IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository)
        {
            _connectorRepository = connectorRepository;
            _groupRepository = groupRepository;
            _chargeStationRepository = chargeStationRepository;
        }

        public Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId)
        {
            return _connectorRepository.GetConnectorsByChargeStation(chargeStationId);
        }

        public Task<ConnectorModel> GetConnectorsById(Guid chargeStationId, int connectorId)
        {
            return _connectorRepository.GetConnectorsById(chargeStationId, connectorId);
        }

        public async Task<ConnectorModel> CreateConnector(Guid groupId, Guid chargeStationId, ConnectorModel newConnector)
        {
            var chargeStation = await _chargeStationRepository.GetByIdAsync(chargeStationId);

            ConnectorModel model = new ConnectorModel();

            try
            {
                model.ChargeStationId = chargeStationId;
                model.MaxCurrent = newConnector.MaxCurrent;
            }
            catch (Exception ex)
            {
            }
            return model;
        }

        public async Task<ConnectorModel> UpdateConnector(Guid groupId, Guid chargeStationId, int connectorId, ConnectorModel updateConnector)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == chargeStationId);
            var connector = chargeStation?.Connectors.FirstOrDefault(x => x.Id == connectorId);

            try
            {
                _connectorRepository.Update(updateConnector);
            }
            catch (Exception ex)
            {
            }

            return updateConnector;
        }
    }
}