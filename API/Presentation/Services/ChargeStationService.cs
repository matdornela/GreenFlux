using API.Domain.Business.Interface;
using API.Domain.Exceptions;
using API.Domain.Models;
using API.Presentation.DTO;
using API.Presentation.Exceptions;
using API.Presentation.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services
{
    public class ChargeStationService : IChargeStationService
    {
        private readonly IChargeStationBusiness _chargeStationBusiness;
        private readonly IMapper _mapper;

        public ChargeStationService(IChargeStationBusiness chargeStationBusiness, IMapper mapper)
        {
            _chargeStationBusiness = chargeStationBusiness;
            _mapper = mapper;
        }

        public async Task<ChargeStationDTO> GetById(Guid id)
        {
            var data = await _chargeStationBusiness.GetById(id);

            if (data == null)
            {
                throw new NotFoundException("Not Found");
            }

            var dataMapped = _mapper.Map<ChargeStationModel, ChargeStationDTO>(data);

            return dataMapped;
        }

        public async Task<List<ChargeStationDTO>> GetChargeStationsByGroupId(Guid groupId)
        {
            var list = _chargeStationBusiness.GetChargeStationsByGroupId(groupId);

            var listMapped = _mapper.Map<List<ChargeStationDTO>>(list);

            return _mapper.Map<List<ChargeStationDTO>>(list);
        }

        public async Task<ChargeStationDTO> Create(ChargeStationDTO chargeStation)
        {
            ChargeStationModel chargeStationCreated = null;
            try
            {
                var chargeStationModel = _mapper.Map<ChargeStationDTO, ChargeStationModel>(chargeStation);

                var chargeStationExists = await _chargeStationBusiness.GetById(chargeStationModel.Id);

                if (chargeStationExists != null)
                {
                    throw new BadRequestException("This charge station is already registered in the database.");
                }

                chargeStationCreated = await _chargeStationBusiness.Create(chargeStationModel);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw new BadRequestException();
                }

                if (e is UIException)
                {
                    throw;
                }

            }

            return _mapper.Map<ChargeStationModel, ChargeStationDTO>(chargeStationCreated);
        }

        public async Task<ChargeStationDTO> Update(ChargeStationDTO chargeStation)
        {
            var model = _mapper.Map<ChargeStationDTO, ChargeStationModel>(chargeStation);

            var chargeStationExists = await _chargeStationBusiness.GetById(model.Id);

            if (chargeStationExists == null)
            {
                throw new NotFoundException();
            }

            var chargeStationUpdated = await _chargeStationBusiness.Update(model);

            return _mapper.Map<ChargeStationModel, ChargeStationDTO>(chargeStationUpdated);
        }

        public async Task Remove(Guid chargeStationId)
        {
            try
            {
                await _chargeStationBusiness.Remove(chargeStationId);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw new NotFoundException(e.Message);
                }
            }
        }
    }
}