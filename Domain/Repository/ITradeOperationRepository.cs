﻿using TradeControl.Domain.Model;

namespace TradeControl.Domain.Repository
{
    public interface ITradeOperationRepository : IRepository<TradeOperation>
    {
        IEnumerable<TradeOperation> GetByUserId(Guid id);
    }
}
