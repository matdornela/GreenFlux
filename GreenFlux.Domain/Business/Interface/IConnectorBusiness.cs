using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.Domain.Models;

namespace GreenFlux.Domain.Business.Interface
{
    public interface IConnectorBusiness
    {
        Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId);

        Task<ConnectorModel> GetById(Guid connectorId);

        Task<ConnectorModel> Create(ConnectorModel model);

        Task<ConnectorModel> Update(ConnectorModel model);

        Task Remove(Guid connectorId);
    }
}