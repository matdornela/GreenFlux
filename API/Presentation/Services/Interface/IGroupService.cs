using API.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Presentation.Services.Interface
{
    public interface IGroupService
    {
        Task<List<GroupDTO>> GetAll();

        Task<GroupDTO> GetById(Guid id);

        Task<GroupDTO> Create(GroupDTO groupDto);

        Task<GroupDTO> Update(GroupDTO updateGroup);

        Task Remove(Guid groupId);
    }
}