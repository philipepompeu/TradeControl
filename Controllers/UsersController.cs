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

        /// <summary>
        /// Retorna a posição consolidada do usuário.
        /// </summary>
        /// <response code="200"> Retorna a posição consolidada do usuário </response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserPositionView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<ActionResult<UserPositionView>> GetUserPositionAsync(Guid id)
        {
            UserPositionView userPosition = await _userService.GetUserPositionAsync(id);

            return Ok(userPosition);// Retorna posição consolidada do usuário
            
        }

        [HttpGet("{id}/average-price")]
        [ProducesResponseType(typeof(AssetPositionView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AssetPositionView>>> GetAveragePrice(Guid id, [FromQuery] string[] tickers)
        {
            List<AssetPositionView> assets = await _userService.GetAveragePriceForTickers(id, tickers);

            return Ok(assets);            
        }
    }
}
