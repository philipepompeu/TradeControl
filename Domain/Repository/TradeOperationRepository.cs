using TradeControl.Domain.Model;

namespace TradeControl.Domain.Repository
{
    public class TradeOperationRepository : Repository<TradeOperation>, IRepository<TradeOperation>
    {
        public TradeOperationRepository(TradeControlDbContext context) : base(context)
        {
        }
    }
}
