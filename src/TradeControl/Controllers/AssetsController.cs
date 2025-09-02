using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TradeControl.Dtos;
using TradeControl.Services;

namespace TradeControl.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Base + "/assets")]
    public class AssetsController : ControllerBase
    {

        private readonly IAssetsService _assetsService;
        
        public AssetsController(IAssetsService assetService)
        {
            _assetsService = assetService;
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetAssetByTickerAsync(string ticker)
        {

            AssetPriceView asset = await _assetsService.ObterCotacaoAsync(ticker);


            return Ok(asset);            
        }
    }
}
