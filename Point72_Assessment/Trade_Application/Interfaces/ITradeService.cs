using Trade_Application.DTOs;

namespace Trade_Application.Interfaces
{
    public interface ITradeService
    {
        Task<TradeResponse> CreateTradeAsync(CreateTradeRequest request);
        Task<IEnumerable<TradeResponse>> GetAllTradesAsync();
        Task<IEnumerable<TradeResponse>> GetTradesBySymbolAsync(string symbol);
        Task<TradeResponse?> GetTradeByIdAsync(int id);
    }
}
