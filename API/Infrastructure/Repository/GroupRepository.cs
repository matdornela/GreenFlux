using API.Domain.Models;
using API.Domain.Repository;
using AutoMapper;
using GreenFlux.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Infrastructure.Repository
{
    public class GroupRepository : Repository<GroupModel>, IGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext Context;

        public GroupRepository(ApplicationDbContext context, IMapper mapper)
            : base(context)
        {
            Context = context;
            _mapper = mapper;
        }

        public async Task<List<GroupModel>> GetGroups()
        {
            var data = await Context.Groups
                .Include(a => a.ChargeStations)
                .ToListAsync();
            return _mapper.Map<List<GroupModel>>(data);
        }

        public async Task<GroupModel> GetGroupById(Guid id)
        {
            var data = await Context.Groups
                .Include(a => a.ChargeStations)
                .SingleOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<GroupModel>(data);
        }
    }
}