using TradeControl.Domain.Model;
using TradeControl.Dtos;

namespace TradeControl.Domain.Repository
{
    public interface ITradeOperationRepository : IRepository<TradeOperation>
    {
        IEnumerable<TradeOperation> GetByUserId(Guid id);
        Task<IEnumerable<UserPositionView>> GetTopPositions();
        Task<decimal> SumBrokerageAsync();
    }
}
