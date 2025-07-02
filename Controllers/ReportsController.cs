using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TradeControl.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Base + "/reports")]
    public class ReportsController : ControllerBase
    {
        [HttpGet("brokerage-total")]
        public IActionResult GetTotalBrokerage() => throw new NotImplementedException();
        [HttpGet("top-positions")]
        public IActionResult GetTopPositions() => throw new NotImplementedException();
        [HttpGet("top-brokerage")]
        public IActionResult GetTopBrokerage() => throw new NotImplementedException();
    }
}
