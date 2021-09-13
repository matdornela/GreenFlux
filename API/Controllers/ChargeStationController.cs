using API.Presentation.DTO;
using API.Presentation.Exceptions;
using API.Presentation.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly IChargeStationService _chargeStationService;

        public ChargeStationController(IChargeStationService chargeStationService)
        {
            _chargeStationService = chargeStationService;
        }

        [HttpGet("{chargeStationId}")]
        public async Task<IActionResult> Get(Guid chargeStationId)
        {
            try
            {
                var chargeStation = await _chargeStationService.GetById(chargeStationId);
                return Ok(chargeStation);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] ChargeStationDTO model)
        {
            try
            {
                var chargeStationCreated = await _chargeStationService.Create(model);
                return Ok(chargeStationCreated);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{chargeStationId}")]
        public async Task<IActionResult> Remove(Guid chargeStationId)
        {
            try
            {
                await _chargeStationService.Remove(chargeStationId);
                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> Update([FromBody] ChargeStationDTO model)
        {
            try
            {
                var chargeStationUpdated = await _chargeStationService.Update(model);
                return Ok(chargeStationUpdated);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}