using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trade_Application.DTOs;
using Trade_Application.Interfaces;

namespace Point72_Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradesController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        /// <summary>
        /// Creates a new trade
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TradeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TradeResponse>> CreateTrade([FromBody] CreateTradeRequest request)
        {
            try
            {
                var response = await _tradeService.CreateTradeAsync(request);
                return CreatedAtAction(nameof(GetTradeById), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all trades
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TradeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TradeResponse>>> GetAllTrades()
        {
            var trades = await _tradeService.GetAllTradesAsync();
            return Ok(trades);
        }

        /// <summary>
        /// Gets a trade by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TradeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TradeResponse>> GetTradeById(int id)
        {
            var trade = await _tradeService.GetTradeByIdAsync(id);

            if (trade == null)
            {
                return NotFound($"Trade with ID {id} not found.");
            }

            return Ok(trade);
        }

        /// <summary>
        /// Gets all trades for a specific symbol
        /// </summary>
        [HttpGet("symbol/{symbol}")]
        [ProducesResponseType(typeof(IEnumerable<TradeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TradeResponse>>> GetTradesBySymbol(string symbol)
        {
            var trades = await _tradeService.GetTradesBySymbolAsync(symbol);
            return Ok(trades);
        }
    }
}
