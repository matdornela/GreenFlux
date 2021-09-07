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
    public class ChargeStationService : IChargeStationService
    {
        private readonly IChargeStationBusiness _chargeStationBusiness;
        private readonly IMapper _mapper;

        public ChargeStationService(IChargeStationBusiness chargeStationBusiness, IMapper mapper)
        {
            _chargeStationBusiness = chargeStationBusiness;
            _mapper = mapper;
        }

        public async Task<ChargeStationDTO> GetChargeStationById(Guid id)
        {
            var data = await _chargeStationBusiness.GetChargeStationById(id);

            var dataMapped = _mapper.Map<ChargeStationModel, ChargeStationDTO>(data);

            return dataMapped;
        }

        public async Task<List<ChargeStationDTO>> GetChargeStationsByGroup(Guid groupId)
        {
            var list = _chargeStationBusiness.GetChargeStationsByGroup(groupId);

            var listMapped = _mapper.Map<List<ChargeStationDTO>>(list);

            return _mapper.Map<List<ChargeStationDTO>>(list);
        }
    }
}