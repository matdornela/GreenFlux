using API.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services.Interface
{
    public interface IConnectorService
    {
        Task<List<ConnectorDTO>> GetAllByChargeStationId(Guid chargeStationId);

        Task<ConnectorDTO> GetById(Guid connectorId);

        Task<ConnectorDTO> Create(ConnectorDTO connectorDTO);

        Task<ConnectorDTO> Update(ConnectorDTO updateConnector);

        Task Remove(Guid connectorId);
    }
}