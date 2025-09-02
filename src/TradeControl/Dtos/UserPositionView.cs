namespace TradeControl.Dtos
{
    public class UserPositionView
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<AssetPositionView> Assets { get; set; }
        public decimal TotalPositionValue { get; set; }
    }
}
