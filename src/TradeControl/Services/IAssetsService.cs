using TradeControl.Dtos;

namespace TradeControl.Services
{
    public interface IAssetsService
    {
        Task<AssetPriceView> ObterCotacaoAsync(string ticker);
    }
}