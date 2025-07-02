using TradeControl.Dtos;

namespace TradeControl.Services
{    
    public class AssetsService: IAssetsService
    {
        private readonly B3Service _b3Service;

        public AssetsService(B3Service b3Service)
        {
            _b3Service = b3Service;
        }

        public Task<AssetPriceView> ObterCotacaoAsync(string ticker)
        {
            return _b3Service.ObterCotacaoAsync(ticker);
        }
    }
}
