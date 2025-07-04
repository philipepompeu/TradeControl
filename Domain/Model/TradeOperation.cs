namespace TradeControl.Domain.Model
{
    public class TradeOperation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AssetId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal BrokerageFee { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
        public Asset Asset { get; set; }
    }
}
