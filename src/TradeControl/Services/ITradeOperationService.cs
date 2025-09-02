using TradeControl.Domain.Model;
using TradeControl.Dtos;

namespace TradeControl.Services
{
    public interface ITradeOperationService
    {
        Task<BrokerageTotal> GetBrokerageTotal();
        Task<List<UserPositionView>> GetTopPositions();
    }
}
