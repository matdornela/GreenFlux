using API.Domain.Models;
using API.Domain.Repositories;
using AutoMapper;
using GreenFlux.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Repositories
{
    public class ChargeStationRepository : Repository<ChargeStationModel>, IChargeStationRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext Context;

        public ChargeStationRepository(ApplicationDbContext context, IMapper mapper)
            : base(context)
        {
            Context = context;
            _mapper = mapper;
        }

        public async Task<ChargeStationModel> GetChargeStationByIdAsync(Guid id)
        {
            var data = await Context.ChargeStations
                .Include(a => a.Connectors)
                .SingleOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<ChargeStationModel>(data);
        }

        public async Task<List<ChargeStationModel>> GetAllChargeStationsByGroupIdAsync(Guid groupId)
        {
            var data = await Context.ChargeStations
                .Where(a => a.GroupId == groupId)
                .Include(a => a.Connectors)
                .ToListAsync();

            return _mapper.Map<List<ChargeStationModel>>(data);
        }
    }
}