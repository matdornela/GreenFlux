using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Repository
{
    public interface IChargeStationRepository : IRepository<ChargeStationModel>
    {
        Task<ChargeStationModel> GetChargeStationByIdAsync(Guid id);

        Task<List<ChargeStationModel>> GetAllChargeStationsByGroupIdAsync(Guid groupId);
    }
}