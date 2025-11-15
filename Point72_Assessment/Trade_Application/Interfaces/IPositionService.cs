using Trade_Application.DTOs;

namespace Trade_Application.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionResponse>> GetAllPositionsAsync();
        Task<PositionResponse?> GetPositionBySymbolAsync(string symbol);
    }
}
