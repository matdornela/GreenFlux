using API.Presentation.DTO;
using API.Presentation.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupDTO>> GetGroup(Guid groupId)
        {
            try
            {
                var group = await _groupService.GetGroupById(groupId);
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost("")]
        public async Task<ActionResult<GroupDTO>> CreateGroup([FromBody] SaveGroupDTO saveGroupDTO)
        {
            var newGroup = await _groupService.CreateGroup(groupToCreate);

            var GroupDTO = _mapper.Map<Group, GroupDTO>(newGroup);

            return Ok(GroupDTO);
        }
    }
}