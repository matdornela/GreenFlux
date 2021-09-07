using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Business.Interface
{
    public interface IChargeStationBusiness
    {
        Task<ChargeStationModel> GetChargeStationById(Guid id);

        Task<List<ChargeStationModel>> GetChargeStationsByGroup(Guid groupId);
    }
}