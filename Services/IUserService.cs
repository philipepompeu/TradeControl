using TradeControl.Dtos;

namespace TradeControl.Services
{
    public interface IUserService
    {
        Task<List<AssetPositionView>> GetAveragePriceForTickers(Guid id, string[] tickers);
        Task<UserPositionView?> GetUserPositionAsync(Guid id);
    }
}
