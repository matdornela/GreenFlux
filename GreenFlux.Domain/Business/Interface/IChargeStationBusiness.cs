using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.Domain.Models;

namespace GreenFlux.Domain.Business.Interface
{
    public interface IChargeStationBusiness
    {
        Task<ChargeStationModel> GetById(Guid id);

        Task<List<ChargeStationModel>> GetChargeStationsByGroupId(Guid groupId);

        Task<ChargeStationModel> Create(ChargeStationModel model);

        Task<ChargeStationModel> Update(ChargeStationModel model);

        Task Remove(Guid chargeStationId);
    }
}