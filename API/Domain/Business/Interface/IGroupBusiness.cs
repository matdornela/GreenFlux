using API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Domain.Business.Interface
{
    public interface IGroupBusiness
    {
        Task<List<GroupModel>> GetGroups();

        Task<GroupModel> GetGroupById(Guid id);

        Task CreateGroup(GroupModel groupModel);

        Task DeleteGroup(Guid groupId);

        Task<GroupModel> UpdateGroup(Guid groupId, GroupModel updateGroup);
    }
}