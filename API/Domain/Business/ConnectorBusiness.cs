using API.Domain.Business.Interface;
using API.Domain.Exceptions;
using API.Domain.Models;
using API.Domain.Repository;
using API.Presentation.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Business
{
    public class ConnectorBusiness : IConnectorBusiness
    {
        private readonly IConnectorRepository _connectorRepository;
        private readonly IChargeStationRepository _chargeStationRepository;

        public ConnectorBusiness(IConnectorRepository connectorRepository, IChargeStationRepository chargeStationRepository)
        {
            _connectorRepository = connectorRepository;
            _chargeStationRepository = chargeStationRepository;
        }

        public Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId)
        {
            return _connectorRepository.GetConnectorsByChargeStation(chargeStationId);
        }

        public async Task<ConnectorModel> Create(ConnectorModel model)
        {
            var chargeStation = await _chargeStationRepository.GetByIdAsync(model.ChargeStationId);

            if (model.ChargeStationId == Guid.Empty)
            {
                throw new BusinessException("A Connector cannot exist in the domain without a Charge Station.");
            }

            if (chargeStation != null && chargeStation != null & chargeStation.Connectors.Count >= 5)
            {
                throw new BusinessException("You can't create more connector to this charge station because it's already exceed the supported limit.");
            }

            var connectorCreated = await _connectorRepository.Create(model);
            return connectorCreated;
        }

        public Task<ConnectorModel> GetById(Guid connectorId)
        {
            return _connectorRepository.GetById(connectorId);
        }

        public async Task<ConnectorModel> Update(ConnectorModel model)
        {
            if (model.MaxCurrent <= 0)
            {
                throw new BusinessException("Value must be greater than zero.");
            }

            var connectorUpdated = await _connectorRepository.Update(model);
            return connectorUpdated;
        }

        public async Task Remove(Guid connectorId)
        {
            try
            {
                _connectorRepository.Remove(connectorId);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw new NotFoundException(e.Message);
                }
            }
        }
    }
}