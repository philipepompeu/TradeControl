using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeControl.Domain.Model;
using TradeControl.Dtos;
using TradeControl.Services;

namespace TradeControl.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Base + "/reports")]
    public class ReportsController : ControllerBase
    {
        
        private readonly ITradeOperationService _tradeOperationService;

        public ReportsController(ITradeOperationService tradeOperationService)
        {
            _tradeOperationService = tradeOperationService;
        }

        [HttpGet("brokerage-total")]
        public async Task<ActionResult<BrokerageTotal>> GetBrokerageTotal() 
        {
            BrokerageTotal total = await _tradeOperationService.GetBrokerageTotal();

            return Ok(total);
        
        }

        [HttpGet("top-positions")]
        public async Task<ActionResult<List<UserPositionView>>> GetTopPositions()
        {
            List<UserPositionView> topPositions = await _tradeOperationService.GetTopPositions();

            return Ok(topPositions);

        }
        [HttpGet("top-brokerage")]
        public IActionResult GetTopBrokerage() => throw new NotImplementedException();
    }
}
