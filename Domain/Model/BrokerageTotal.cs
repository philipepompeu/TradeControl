namespace TradeControl.Domain.Model
{
    public class BrokerageTotal
    {
        public decimal Total {  get; set; }
        public DateTime QueryDate { get; set; } = DateTime.UtcNow;
    }
}
