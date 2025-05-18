using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.IService;
using Shared;
using Shared.Dtos.Basket;
using Shared.Dtos.Products;

namespace Presentation.Controllers
{
    public class BasketsController(IServiceManager _serviceManager)
        : APIController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
        {
            var result = await _serviceManager.BasketService.GetAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> UpdateBasketAsync(BasketDto dto)
        {
            var result = await _serviceManager.BasketService.UpdateAsync(dto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<BasketDto>> DeleteAsync(string id)
        {
            var result = await _serviceManager.BasketService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
