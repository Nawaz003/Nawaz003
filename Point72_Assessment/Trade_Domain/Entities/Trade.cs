namespace Trade_Domain.Entities
{
    public class Trade
    {
        public int Id { get; private set; }
        public string Symbol { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
        public TradeType Type { get; private set; }
        public DateTime TradeDate { get; private set; }

        private Trade() { } // EF Core

        public Trade(string symbol, decimal quantity, decimal price, TradeType type)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Symbol cannot be empty", nameof(symbol));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero", nameof(price));

            Symbol = symbol.ToUpper();
            Quantity = quantity;
            Price = price;
            Type = type;
            TradeDate = DateTime.UtcNow;
        }
    }

    public enum TradeType
    {
        Buy,
        Sell
    }
}
