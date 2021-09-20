using AutoMapper;
using GreenFlux.Domain.Business.Interface;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Domain.Models;
using GreenFlux.Presentation.DTO;
using GreenFlux.Presentation.Exceptions;
using GreenFlux.Presentation.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenFlux.Presentation.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly IConnectorBusiness _connectorBusiness;
        private readonly IChargeStationBusiness _chargeStationBusiness;
        private readonly IMapper _mapper;

        public ConnectorService(IConnectorBusiness connectorBusiness, IMapper mapper, IChargeStationBusiness chargeStationBusiness)
        {
            _connectorBusiness = connectorBusiness;
            _mapper = mapper;
            _chargeStationBusiness = chargeStationBusiness;
        }

        public async Task<List<ConnectorDTO>> GetAllByChargeStationId(Guid chargeStationId)
        {
            var list = await _connectorBusiness.GetConnectorsByChargeStation(chargeStationId);

            return _mapper.Map<List<ConnectorDTO>>(list);
        }

        public async Task<ConnectorDTO> GetById(Guid connectorId)
        {
            var connector = await _connectorBusiness.GetById(connectorId);

            if (connector == null)
            {
                throw new NotFoundException("Not Found");
            }

            return _mapper.Map<ConnectorModel, ConnectorDTO>(connector);
        }

        public async Task<ConnectorDTO> Create(ConnectorDTO connectorDTO)
        {
            try
            {
                var connectorExists = await _connectorBusiness.GetById(connectorDTO.Id);

                if (connectorExists != null)
                {
                    throw new BadRequestException("This connector already exists in the database");
                }

                var chargeStation = await _chargeStationBusiness.GetById(connectorDTO.ChargeStationId);

                if (chargeStation == null)
                {
                    throw new BadRequestException("There's not charge station registered for this connector");
                }
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw;
                }
                if (e is UIException)
                {
                    throw;
                }
                if (e is BadRequestException)
                {
                    throw;
                }
            }

            var connectorModel = _mapper.Map<ConnectorModel>(connectorDTO);

            var connectorCreated = await _connectorBusiness.Create(connectorModel);

            return _mapper.Map<ConnectorModel, ConnectorDTO>(connectorCreated);
        }

        public async Task<ConnectorDTO> Update(ConnectorDTO updateConnector)
        {
            try
            {
                var connectorModel = _mapper.Map<ConnectorModel>(updateConnector);

                var connectorExists = await _connectorBusiness.GetById(connectorModel.Id);

                if (connectorExists == null)
                {
                    throw new NotFoundException();
                }

                connectorModel.ChargeStationId = connectorExists.ChargeStationId;

                var connectorUpdated = await _connectorBusiness.Update(connectorModel);

                return _mapper.Map<ConnectorModel, ConnectorDTO>(connectorUpdated);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw new BadRequestException(e.Message);
                }
                if (e is NotFoundException)
                {
                    throw;
                }
            }

            return await Task.FromResult<ConnectorDTO>(null);
        }

        public async Task Remove(Guid connectorId)
        {
            try
            {
                var connectorExists = _connectorBusiness.GetById(connectorId);

                if (connectorExists == null)
                {
                    throw new NotFoundException();
                }

                await _connectorBusiness.Remove(connectorId);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw;
                }
                if (e is NotFoundException)
                {
                    throw;
                }
            }
        }
    }
}