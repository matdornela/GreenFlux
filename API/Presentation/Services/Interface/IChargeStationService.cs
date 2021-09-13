using API.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services.Interface
{
    public interface IChargeStationService
    {
        Task<ChargeStationDTO> GetById(Guid id);

        Task<List<ChargeStationDTO>> GetChargeStationsByGroupId(Guid groupId);

        Task<ChargeStationDTO> Create(ChargeStationDTO chargeStation);

        Task<ChargeStationDTO> Update(ChargeStationDTO chargeStation);

        Task Remove(Guid chargeStationId);
    }
}