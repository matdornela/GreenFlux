using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repository
{
    public interface IConnectorRepository : IRepository<ConnectorModel>
    {
        Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId);

        Task<ConnectorModel> GetConnectorsById(Guid chargeStationId, int connectorId);
    }
}