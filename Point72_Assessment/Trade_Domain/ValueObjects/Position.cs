namespace Trade_Domain.ValueObjects
{
    public class Position
    {
        public string Symbol { get; }
        public decimal TotalQuantity { get; }
        public decimal AveragePrice { get; }
        public decimal TotalValue { get; }

        public Position(string symbol, decimal totalQuantity, decimal averagePrice, decimal totalValue)
        {
            Symbol = symbol;
            TotalQuantity = totalQuantity;
            AveragePrice = Math.Round(averagePrice, 2);
            TotalValue = Math.Round(totalValue, 2);
        }
    }
}
