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
    public class ConnectorRepository : Repository<ConnectorModel>, IConnectorRepository
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext Context;

        public ConnectorRepository(ApplicationDbContext context, IMapper mapper)
            : base(context)
        {
            Context = context;
            _mapper = mapper;
        }

        public async Task<List<ConnectorModel>> GetAllConnectorsByChargeStationId(Guid chargeStationId)
        {
            var data = await Context.ChargeStations
                 .Where(a => a.Id == chargeStationId)
                 .SelectMany(a => a.Connectors)
                 .ToListAsync();

            return _mapper.Map<List<ConnectorModel>>(data);
        }

        public async Task<ConnectorModel> GetConnectorsById(Guid chargeStationId, int id)
        {
            var data = await Context.ChargeStations
                .Where(a => a.Id == chargeStationId)
                .SelectMany(a => a.Connectors)
                .SingleOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<ConnectorModel>(data);
        }
    }
}