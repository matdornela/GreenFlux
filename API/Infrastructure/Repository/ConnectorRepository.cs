using API.Domain.Exceptions;
using API.Domain.Models;
using API.Domain.Repository;
using API.Infrastructure.Models;
using AutoMapper;
using GreenFlux.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure.Repository
{
    public class ConnectorRepository : IConnectorRepository
    {
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext Context;

        public ConnectorRepository(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public async Task<List<ConnectorModel>> GetConnectorsByChargeStation(Guid chargeStationId)
        {
            var data = await Context.ChargeStations
                 .Where(a => a.Id == chargeStationId)
                 .SelectMany(a => a.Connectors)
                 .ToListAsync();

            return _mapper.Map<List<ConnectorModel>>(data);
        }

        public async Task<ConnectorModel> GetById(Guid id)
        {
            var data = await Context.Connectors
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<ConnectorModel>(data);
        }

        public async Task<ConnectorModel> Create(ConnectorModel model)
        {
            var connector = _mapper.Map<ConnectorModel, Connector>(model);
            await Context.Connectors.AddAsync(connector);
            await Context.SaveChangesAsync();
            return model;
        }

        public async Task<ConnectorModel> Update(ConnectorModel model)
        {
            var connector = _mapper.Map<ConnectorModel, Connector>(model);
            Context.Update(connector);
            await Context.SaveChangesAsync();
            return model;
        }

        public void Remove(Guid connectorId)
        {
            var connector = Context.Connectors.SingleOrDefault(x => x.Id == connectorId);
            if (connector == null) throw new BusinessException("Not Found");
            Context.Remove(connector);
            Context.SaveChanges();
        }
    }
}