using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure.Models;
using API.Presentation.DTO;
using API.Presentation.Services.Interface;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<ActionResult<ConnectorDTO>> GetConnector(Guid chargeStationId, int connectorId)
        {
            var connector = await _connectorService.GetConnectorsById(chargeStationId, connectorId);
            return Ok(connector);
        }
    }
}
