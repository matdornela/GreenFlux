using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repositories
{
    public interface IConnectorRepository : IRepository<ConnectorModel>
    {
        Task<List<ConnectorModel>> GetAllConnectorsByChargeStationId(Guid chargeStationId);

        Task<ConnectorModel> GetConnectorsById(Guid chargeStationId, int connectorId);
    }
}