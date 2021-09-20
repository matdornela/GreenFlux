using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenFlux.Domain.Models;

namespace GreenFlux.Domain.Repository
{
    public interface IGroupRepository
    {
        Task<List<GroupModel>> GetAll();

        Task<GroupModel> GetById(Guid id);

        Task<GroupModel> Create(GroupModel model);

        Task<GroupModel> Update(GroupModel model);

        void Delete(Guid groupId);
    }
}