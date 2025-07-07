using Microsoft.EntityFrameworkCore;
using TradeControl.Domain.Model;
using TradeControl.Dtos;

namespace TradeControl.Domain.Repository
{
    public class TradeOperationRepository : Repository<TradeOperation>, ITradeOperationRepository
    {
        public TradeOperationRepository(TradeControlDbContext context) : base(context)
        {
        }

        public IEnumerable<TradeOperation> GetByUserId(Guid id)
        {
            return this._dbSet.Include(t => t.Asset).Where(t => t.UserId == id).ToList();
        }

        public async Task<IEnumerable<UserPositionView>> GetTopPositions()
        {
            return await _dbSet.
        }

        public async Task<decimal> SumBrokerageAsync() => await _dbSet.SumAsync(t => t.BrokerageFee);
    }
}
