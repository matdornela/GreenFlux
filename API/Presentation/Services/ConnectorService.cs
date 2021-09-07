using API.Domain.Business.Interface;
using API.Domain.Models;
using API.Presentation.DTO;
using API.Presentation.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly IConnectorBusiness _connectorBusiness;
        private readonly IMapper _mapper;

        public ConnectorService(IConnectorBusiness connectorBusiness, IMapper mapper)
        {
            _connectorBusiness = connectorBusiness;
            _mapper = mapper;
        }

        public async Task<List<ConnectorDTO>> GetConnectorsByChargeStation(Guid chargeStationId)
        {
            var list = _connectorBusiness.GetConnectorsByChargeStation(chargeStationId);

            return _mapper.Map<List<ConnectorDTO>>(list);
        }

        public async Task<ConnectorDTO> GetConnectorsById(Guid chargeStationId, int connectorId)
        {
            var data = await _connectorBusiness.GetConnectorsById(chargeStationId, connectorId);

            return _mapper.Map<ConnectorModel, ConnectorDTO>(data);
        }


    }
}