using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.Domain.Models;

namespace GreenFlux.Domain.Repository
{
    public interface IChargeStationRepository
    {
        Task<ChargeStationModel> GetByIdAsync(Guid id);

        Task<List<ChargeStationModel>> GetAllChargeStationsByGroupIdAsync(Guid groupId);

        Task<ChargeStationModel> Create(ChargeStationModel model);

        Task<ChargeStationModel> Update(ChargeStationModel model);

        void Remove(Guid chargeStationId);
    }
}