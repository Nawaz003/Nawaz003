using Microsoft.AspNetCore.Mvc;
using Trade_Application.Interfaces;
using Trade_Domain.ValueObjects;


namespace Point72_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions()
        {
            var positions = await _positionService.GetAllPositionsAsync();
            return Ok(positions);
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<Position>> GetPositionBySymbol(string symbol)
        {
            var position = await _positionService.GetPositionBySymbolAsync(symbol);

            if (position == null)
            {
                return NotFound($"No position found for symbol {symbol}.");
            }

            return Ok(position);
        }

    }
}
