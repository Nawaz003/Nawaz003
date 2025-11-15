using Trade_Domain.Entities;

namespace Trade_Domain.Repositories
{
    public interface ITradeRepository
    {
        Task<Trade> AddAsync(Trade trade);
        Task<Trade?> GetByIdAsync(int id);
        Task<IEnumerable<Trade>> GetAllAsync();
        Task<IEnumerable<Trade>> GetBySymbolAsync(string symbol);
    }
}
