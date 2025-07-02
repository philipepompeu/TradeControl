using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TradeControl.Controllers
{
    [Route(ApiRoutes.Base + "/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetUserPosition(Guid id)
        {
            // Retorna posição consolidada do usuário
            throw new NotImplementedException();
        }

        [HttpGet("{id}/average-price")]
        public IActionResult GetAveragePrice(Guid id, [FromQuery] string[] tickers)
        {
            // Retorna preço médio dos ativos informados
            throw new NotImplementedException();
        }
    }
}
