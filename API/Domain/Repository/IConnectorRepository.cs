using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Repository
{
    public interface IConnectorRepository
    {
        Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId);

        Task<ConnectorModel> GetById(Guid connectorId);

        Task<ConnectorModel> Create(ConnectorModel model);

        Task<ConnectorModel> Update(ConnectorModel model);

        void Remove(Guid connectorId);
    }
}