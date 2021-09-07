using API.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services.Interface
{
    public interface IChargeStationService
    {
        Task<ChargeStationDTO> GetChargeStationById(Guid id);

        Task<List<ChargeStationDTO>> GetChargeStationsByGroup(Guid groupId);
    }
}