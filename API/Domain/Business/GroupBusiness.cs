using API.Domain.Business.Interface;
using API.Domain.Models;
using API.Domain.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Business
{
    public class GroupBusiness : IGroupBusiness
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupBusiness(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public Task<List<GroupModel>> GetGroups()
        => _groupRepository.GetGroups();

        public async Task<GroupModel> GetGroupById(Guid id)
        {
            var data = await _groupRepository.GetGroupById(id);
            return data;
        }

        public async Task CreateGroup(GroupModel groupModel)
        {
            await _groupRepository.AddAsync(groupModel);
        }


        public async Task DeleteGroup(Guid groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);

            if (group != null)
            {
                if (group.ChargeStations != null)
                {
                    foreach (var chargeStation in group.ChargeStations)
                    {
                        group.ChargeStations.Remove(chargeStation);
                    }
                }
            }
            else
            {
                throw new Exception($"Group not found with groupId: {groupId}");
            }
            _groupRepository.Remove(group);
        }

        public async Task<GroupModel> UpdateGroup(Guid groupId, GroupModel updateGroup)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);

            //validate if capacity greater than 0;
            _groupRepository.Update(updateGroup);

            return group;
        }
    }
}