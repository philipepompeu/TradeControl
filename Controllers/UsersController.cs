using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradeControl.Dtos;
using TradeControl.Services;

namespace TradeControl.Controllers
{
    [Route(ApiRoutes.Base + "/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        { 
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPositionView>> GetUserPositionAsync(Guid id)
        {
            UserPositionView userPosition = await _userService.GetUserPositionAsync(id);

            if(userPosition == null)
                return NotFound(id);

            return Ok(userPosition);// Retorna posição consolidada do usuário
            
        }

        [HttpGet("{id}/average-price")]
        public async Task<ActionResult<List<AssetPositionView>>> GetAveragePrice(Guid id, [FromQuery] string[] tickers)
        {

            List<AssetPositionView> assets = await _userService.GetAveragePriceForTickers(id, tickers);

            if(assets == null)
                return NotFound(id);

            return Ok(assets);            
        }
    }
}
