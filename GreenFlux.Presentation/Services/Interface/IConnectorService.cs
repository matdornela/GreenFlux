using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.Presentation.DTO;

namespace GreenFlux.Presentation.Services.Interface
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