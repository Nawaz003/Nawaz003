using Microsoft.EntityFrameworkCore;
using Trade_Domain.Entities;
using Trade_Domain.Repositories;
using Trade_Infrastructure.Persistence;

namespace Trade_Infrastructure.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly TradeDbContext _context;

        public TradeRepository(TradeDbContext context)
        {
            _context = context;
        }

        public async Task<Trade> AddAsync(Trade trade)
        {
            await _context.Trades.AddAsync(trade);
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task<Trade?> GetByIdAsync(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task<IEnumerable<Trade>> GetAllAsync()
        {
            return await _context.Trades
                .OrderByDescending(t => t.TradeDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Trade>> GetBySymbolAsync(string symbol)
        {
            return await _context.Trades
                .Where(t => t.Symbol.ToLower() == symbol.ToLower())
                .OrderByDescending(t => t.TradeDate)
                .ToListAsync();
        }
    }
}
