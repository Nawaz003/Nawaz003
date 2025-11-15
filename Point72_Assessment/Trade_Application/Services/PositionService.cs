using Trade_Application.DTOs;
using Trade_Application.Interfaces;
using Trade_Domain.Entities;
using Trade_Domain.Repositories;
using Trade_Domain.ValueObjects;

namespace Trade_Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly ITradeRepository _tradeRepository;

        public PositionService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<IEnumerable<PositionResponse>> GetAllPositionsAsync()
        {
            var trades = await _tradeRepository.GetAllAsync();

            var positions = trades
                .GroupBy(t => t.Symbol)
                .Select(g => CalculatePosition(g.Key, g.ToList()))
                .Where(p => p.TotalQuantity != 0)
                .Select(MapToResponse);

            return positions;
        }

        public async Task<PositionResponse?> GetPositionBySymbolAsync(string symbol)
        {
            var trades = await _tradeRepository.GetBySymbolAsync(symbol);
            var tradeList = trades.ToList();

            if (!tradeList.Any())
                return null;

            var position = CalculatePosition(symbol, tradeList);
            return position.TotalQuantity == 0 ? null : MapToResponse(position);
        }

        private Position CalculatePosition(string symbol, List<Trade> trades)
        {
            decimal totalQuantity = 0;
            decimal totalCost = 0;

            foreach (var trade in trades.OrderBy(t => t.TradeDate))
            {
                if (trade.Type == TradeType.Buy)
                {
                    totalCost += trade.Quantity * trade.Price;
                    totalQuantity += trade.Quantity;
                }
                else // Sell
                {
                    if (totalQuantity > 0)
                    {
                        var avgPrice = totalCost / totalQuantity;
                        totalCost -= trade.Quantity * avgPrice;
                    }
                    totalQuantity -= trade.Quantity;
                }
            }

            var averagePrice = totalQuantity != 0 ? totalCost / totalQuantity : 0;

            return new Position(symbol, totalQuantity, averagePrice, totalCost);
        }

        private PositionResponse MapToResponse(Position position)
        {
            return new PositionResponse
            {
                Symbol = position.Symbol,
                TotalQuantity = position.TotalQuantity,
                AveragePrice = position.AveragePrice,
                TotalValue = position.TotalValue
            };
        }
    }
}
