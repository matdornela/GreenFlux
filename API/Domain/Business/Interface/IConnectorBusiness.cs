using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Business.Interface
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