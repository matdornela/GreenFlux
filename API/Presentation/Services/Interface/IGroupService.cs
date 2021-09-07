using API.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services.Interface
{
    public interface IGroupService
    {
        Task<List<GroupDTO>> GetGroups();

        Task<GroupDTO> GetGroupById(Guid id);

        Task CreateGroup(GroupDTO groupDto);

        Task<GroupDTO> UpdateGroup(Guid groupId, GroupDTO updateGroup);
    }
}