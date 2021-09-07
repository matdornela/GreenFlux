using API.Domain.Business.Interface;
using API.Presentation.DTO;
using API.Presentation.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Presentation.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupBusiness _groupBusiness;
        private readonly IMapper _mapper;

        public GroupService(IGroupBusiness groupBusiness, IMapper mapper)
        {
            _groupBusiness = groupBusiness;
            _mapper = mapper;
        }

        public async Task<List<GroupDTO>> GetGroups()
        {
            var list = _groupBusiness.GetGroups();

            return _mapper.Map<List<GroupDTO>>(list);
        }

        public async Task<GroupDTO> GetGroupById(Guid id)
        {
            var data = await _groupBusiness.GetGroupById(id);

            var dataMapped = _mapper.Map<GroupModel, GroupDTO>(data);

            return dataMapped;
        }

        public async Task CreateGroup(GroupDTO groupDto)
        {
            var groupModel = _mapper.Map<GroupDTO, GroupModel>(groupDto);

            await _groupBusiness.CreateGroup(groupModel);
        }

        public async Task<GroupDTO> UpdateGroup(Guid groupId, GroupDTO updateGroup)
        {
            var model = _mapper.Map<GroupDTO, GroupModel>(updateGroup);
            var data = await _groupBusiness.UpdateGroup(groupId, model);
            return _mapper.Map<GroupDTO>(data);
        } 
    }
}