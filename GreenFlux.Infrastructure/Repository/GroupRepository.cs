using AutoMapper;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Domain.Models;
using GreenFlux.Domain.Repository;
using GreenFlux.Infrastructure.DbContexts;
using GreenFlux.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.Infrastructure.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext Context;

        public GroupRepository(ApplicationDbContext context, IMapper mapper)

        {
            Context = context;
            _mapper = mapper;
        }

        public async Task<List<GroupModel>> GetAll()
        {
            var data = await Context.Groups
                .AsNoTracking()
                .Include(a => a.ChargeStations)
                .ToListAsync();
            return _mapper.Map<List<GroupModel>>(data);
        }

        public async Task<GroupModel> GetById(Guid id)
        {
            var data = await Context.Groups
                .AsNoTracking()
                .Include(x => x.ChargeStations)
                .ThenInclude(x => x.Connectors)
                .SingleOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<GroupModel>(data);
        }

        public async Task<GroupModel> Create(GroupModel model)
        {
            var group = _mapper.Map<GroupModel, Group>(model);
            await Context.Groups.AddAsync(group);
            await Context.SaveChangesAsync();
            return model;
        }

        public async Task<GroupModel> Update(GroupModel model)
        {
            var group = _mapper.Map<GroupModel, Group>(model);
            Context.Update(group);
            await Context.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid groupId)
        {
            var group = Context.Groups.SingleOrDefault(x => x.Id == groupId);
            if (group == null) throw new BusinessException("Not Found");
            Context.Remove(group);
            Context.SaveChanges();
        }
    }
}