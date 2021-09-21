using System;
using System.Threading.Tasks;
using GreenFlux.Presentation.DTO;
using GreenFlux.Presentation.Exceptions;
using GreenFlux.Presentation.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.API.Controllers
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
        public async Task<IActionResult> Get(Guid groupId)
        {
            try
            {
                var group = await _groupService.GetById(groupId);
                return Ok(group);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] GroupDTO model)
        {
            try
            {
                var groupCreated = await _groupService.Create(model);
                return Ok(groupCreated);
            }
            catch (UIException e)
            {
                return BadRequest(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> Remove(Guid groupId)
        {
            try
            {
                await _groupService.Remove(groupId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] GroupDTO model)
        {
            try
            {
                var groupUpdated = await _groupService.Update(model);
                return Ok(groupUpdated);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (BadRequestException)
            {
                return BadRequest();
            }
        }
    }
}