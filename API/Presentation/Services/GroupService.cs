using API.Domain.Business.Interface;
using API.Domain.Exceptions;
using API.Domain.Models;
using API.Presentation.DTO;
using API.Presentation.Exceptions;
using API.Presentation.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<GroupDTO>> GetAll()
        {
            var list = _groupBusiness.GetAll();

            return _mapper.Map<List<GroupDTO>>(list);
        }

        public async Task<GroupDTO> GetById(Guid id)
        {
            try
            {
                var data = await _groupBusiness.GetById(id);

                if (data.Id == Guid.Empty)
                {
                    throw new NotFoundException();
                }

                var dataMapped = _mapper.Map<GroupModel, GroupDTO>(data);

                return dataMapped;
            }
            catch (Exception e)
            {
                if (e is NotFoundException)
                {
                    throw;
                }
            }

            return await Task.FromResult<GroupDTO>(null);
        }

        public async Task<GroupDTO> Create(GroupDTO groupDto)
        {
            try
            {
                var groupModel = _mapper.Map<GroupDTO, GroupModel>(groupDto);

                var groupExists = await _groupBusiness.GetById(groupModel.Id);

                if (groupExists != null)
                {
                    throw new BadRequestException("Group already exists in the database.");
                }

                var groupCreated = await _groupBusiness.Create(groupModel);

                return _mapper.Map<GroupModel, GroupDTO>(groupCreated);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw;
                }
                if (e is UIException)
                {
                    throw;
                }
                if (e is BadRequestException)
                {
                    throw;
                }
            }

            return await Task.FromResult<GroupDTO>(null);
        }

        public async Task<GroupDTO> Update(GroupDTO updateGroup)
        {
            try
            {
                var groupModel = _mapper.Map<GroupDTO, GroupModel>(updateGroup);

                var groupExists = await _groupBusiness.GetById(groupModel.Id);

                if (groupExists == null)
                {
                    throw new NotFoundException();
                }

                var groupUpdated = await _groupBusiness.Update(groupModel);

                return _mapper.Map<GroupModel, GroupDTO>(groupUpdated);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw new BadRequestException(e.Message);
                }
                if (e is NotFoundException)
                {
                    throw;
                }
            }

            return await Task.FromResult<GroupDTO>(null);
        }

        public async Task Remove(Guid groupId)
        {
            try
            {
                var groupExists = await _groupBusiness.GetById(groupId);

                if (groupExists == null)
                {
                    throw new NotFoundException();
                }

                await _groupBusiness.Remove(groupId);
            }
            catch (Exception e)
            {
                if (e is BusinessException)
                {
                    throw;
                }
                if (e is NotFoundException)
                {
                    throw;
                }
            }
        }
    }
}