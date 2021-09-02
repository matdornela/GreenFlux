using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repositories
{
    public interface IGroupRepository : IRepository<GroupModel>
    {
        Task<List<GroupModel>> GetAllWithGroupsAsync();
        Task<GroupModel> GetWithGroupsByIdAsync(Guid id);
    }
}
