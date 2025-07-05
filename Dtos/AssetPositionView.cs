namespace TradeControl.Dtos
{
    public class AssetPositionView
    {
        public string Ticker { get; set; }
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PositionValue { get; set; }
    }
}
