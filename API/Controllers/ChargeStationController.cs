using API.Presentation.DTO;
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
        public async Task<ActionResult<ChargeStationDTO>> GetChargeStation(Guid groupId, Guid chargeStationId)
        {
            var chargeStation = await _chargeStationService.GetChargeStationById(chargeStationId);
            return Ok(chargeStation);
        }
    }
}