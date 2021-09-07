using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repository
{
    public interface IGroupRepository : IRepository<GroupModel>
    {
        Task<List<GroupModel>> GetGroups();
        Task<GroupModel> GetGroupById(Guid id);
    }
}
