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

        public async Task<List<UserPositionView>> GetTopPositions()
        {

            var operacoes = await _dbSet
                .GroupBy(op => new { op.UserId, op.AssetId })
                .Select(grupo => new
                {
                    grupo.Key.UserId,
                    TotalAtivo = grupo.Sum(o => o.Quantity) *
                                 (grupo.Sum(o => o.Quantity * o.UnitPrice) / grupo.Sum(o => o.Quantity))
                }).ToListAsync();

            var resultado = operacoes
                .GroupBy(x => x.UserId)
                .Select(g => new UserPositionView
                {
                    UserId = g.Key,
                    TotalPositionValue = g.Sum(x => x.TotalAtivo),
                    Assets = new List<AssetPositionView>()
                })
                .OrderByDescending(g => g.TotalPositionValue)
                .Take(10)
                .ToList();


            return resultado;
        }

        public async Task<decimal> SumBrokerageAsync() => await _dbSet.SumAsync(t => t.BrokerageFee);
    }
}
