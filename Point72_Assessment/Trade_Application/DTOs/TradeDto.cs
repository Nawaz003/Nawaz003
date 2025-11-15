namespace Trade_Application.DTOs
{
    public class CreateTradeRequest
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class TradeResponse
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime TradeDate { get; set; }
    }
}
