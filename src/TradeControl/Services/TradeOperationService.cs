using TradeControl.Domain.Model;
using TradeControl.Domain.Repository;
using TradeControl.Dtos;

namespace TradeControl.Services
{
    public class TradeOperationService : ITradeOperationService
    {
        private readonly ITradeOperationRepository _repository;

        public TradeOperationService(ITradeOperationRepository tradeOperationRepository)
        {
            _repository = tradeOperationRepository;
        }

        public async Task<BrokerageTotal> GetBrokerageTotal()
        {
            decimal brokerageTotal  = await _repository.SumBrokerageAsync();

            return new BrokerageTotal { Total = brokerageTotal };
        }

        public async Task<List<UserPositionView>> GetTopPositions()
        {
            List<UserPositionView> topPositions = await _repository.GetTopPositions();


            return topPositions;
        }
    }
}
