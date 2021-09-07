using API.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services.Interface
{
    public interface IConnectorService
    {
        Task<List<ConnectorDTO>> GetConnectorsByChargeStation(Guid chargeStationId);

        Task<ConnectorDTO> GetConnectorsById(Guid chargeStationId, int connectorId);
    }
}