namespace Trade_Application.DTOs
{
    public class PositionResponse
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal TotalQuantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal TotalValue { get; set; }
    }
}
