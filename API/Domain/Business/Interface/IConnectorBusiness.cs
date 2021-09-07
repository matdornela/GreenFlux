using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Business.Interface
{
    public interface IConnectorBusiness
    {
        Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId);

        Task<ConnectorModel> GetConnectorsById(Guid chargeStationId, int connectorId);

        Task<ConnectorModel> CreateConnector(Guid groupId, Guid chargeStationId, ConnectorModel newConnector);

        Task<ConnectorModel> UpdateConnector(Guid groupId, Guid chargeStationId, int connectorId,
            ConnectorModel updateConnector);
    }
}