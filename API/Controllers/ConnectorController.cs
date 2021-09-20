using AutoMapper;
using GreenFlux.Presentation.DTO;
using GreenFlux.Presentation.Exceptions;
using GreenFlux.Presentation.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly IConnectorService _connectorService;
        private readonly IMapper _mapper;

        public ConnectorController(IConnectorService connectorService, IMapper mapper)
        {
            _connectorService = connectorService;
            _mapper = mapper;
        }

        [HttpGet("{connectorId}")]
        public async Task<IActionResult> Get(Guid connectorId)
        {
            try
            {
                var connector = await _connectorService.GetById(connectorId);
                return Ok(connector);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ConnectorDTO model)
        {
            try
            {
                var groupCreated = await _connectorService.Create(model);
                return Ok(groupCreated);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{ConnectorId}")]
        public async Task<IActionResult> Remove(Guid ConnectorId)
        {
            try
            {
                await _connectorService.Remove(ConnectorId);
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
        public async Task<IActionResult> Update([FromBody] ConnectorDTO model)
        {
            try
            {
                var connectorUpdated = await _connectorService.Update(model);
                return Ok(connectorUpdated);
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