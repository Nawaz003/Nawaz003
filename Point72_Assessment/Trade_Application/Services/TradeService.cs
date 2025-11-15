using Trade_Application.DTOs;
using Trade_Application.Interfaces;
using Trade_Infrastructure.Repositories;
using Trade_Domain.Entities;
using Trade_Domain.Repositories;


namespace Trade_Application.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repository;

        public TradeService(ITradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<TradeResponse> CreateTradeAsync(CreateTradeRequest request)
        {
            if (!Enum.TryParse<TradeType>(request.Type, true, out var tradeType))
            {
                throw new ArgumentException("Invalid trade type. Must be 'Buy' or 'Sell'.");
            }

            var trade = new Trade(request.Symbol, request.Quantity, request.Price, tradeType);
            var createdTrade = await _repository.AddAsync(trade);

            return MapToResponse(createdTrade);
        }

        public async Task<IEnumerable<TradeResponse>> GetAllTradesAsync()
        {
            var trades = await _repository.GetAllAsync();
            return trades.Select(MapToResponse);
        }

        public async Task<IEnumerable<TradeResponse>> GetTradesBySymbolAsync(string symbol)
        {
            var trades = await _repository.GetBySymbolAsync(symbol);
            return trades.Select(MapToResponse);
        }

        public async Task<TradeResponse?> GetTradeByIdAsync(int id)
        {
            var trade = await _repository.GetByIdAsync(id);
            return trade != null ? MapToResponse(trade) : null;
        }

        private TradeResponse MapToResponse(Trade trade)
        {
            return new TradeResponse
            {
                Id = trade.Id,
                Symbol = trade.Symbol,
                Quantity = trade.Quantity,
                Price = trade.Price,
                Type = trade.Type.ToString(),
                TradeDate = trade.TradeDate
            };
        }
    }
}
