using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TradeControl.Domain.Model;
using TradeControl.Domain.Repository;
using TradeControl.Dtos;

namespace TradeControl.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITradeOperationRepository _tradeOperationRepository;
        private readonly B3Service _b3Service;
        public UserService(IUserRepository userRepository, ITradeOperationRepository tradeOperationRepository, B3Service b3Service)
        {
            _userRepository = userRepository;
            _tradeOperationRepository = tradeOperationRepository;
            _b3Service = b3Service;
        }

        public async Task<List<AssetPositionView>> GetAveragePriceForTickers(Guid id, string[] tickers)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            IEnumerable<TradeOperation> operations = _tradeOperationRepository.GetByUserId(user.Id).Where( op => tickers.Contains(op.Asset.Ticker) );

            IEnumerable<AssetPositionView> assetsPosition = operationsToAssetPositions(operations);

            return assetsPosition.ToList();

        }

        public async Task<UserPositionView?> GetUserPositionAsync(Guid id)
        {

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            UserPositionView userPositionView = new UserPositionView { UserId = user.Id, UserName = user.Name };

            IEnumerable<TradeOperation> operations = _tradeOperationRepository.GetByUserId(user.Id);
            IEnumerable<AssetPositionView> assetsPosition = operationsToAssetPositions(operations);

            userPositionView.Assets = assetsPosition.ToList();

            decimal totalPositionValue = 0;
            foreach (var asset in userPositionView.Assets)
            {
                var currentValue = await _b3Service.ObterCotacaoAsync(asset.Ticker);

                if (currentValue == null) continue;

                asset.CurrentPrice = currentValue.Price ?? 0;

                asset.PositionValue = Math.Round(asset.Quantity * asset.CurrentPrice, 2);

                totalPositionValue += asset.PositionValue;
            }

            userPositionView.TotalPositionValue = totalPositionValue;

            return userPositionView;
        }

        private static IEnumerable<AssetPositionView> operationsToAssetPositions(IEnumerable<TradeOperation> operations)
        {
            return operations
                            .GroupBy(o => o.Asset.Ticker)
                            .Select(grupo => new AssetPositionView
                                {
                                    Ticker = grupo.Key,
                                    Quantity = grupo.Sum(o => o.Quantity),
                                    AveragePrice = Math.Round(grupo.Sum(o => o.UnitPrice * o.Quantity) / grupo.Sum(o => o.Quantity),2)
                                }
                            );
        }
    }
}
