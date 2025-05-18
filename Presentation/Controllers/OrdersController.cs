using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using ServiceAbstraction.IService;
using Shared.Dtos.Orders;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager)
                : APIController
    {

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest request)
            => Ok(await _serviceManager.OrderService.CreateAsync(request, GetUserEmailFromToken()));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders() => Ok(await _serviceManager.OrderService.GetAllAsync(GetUserEmailFromToken()));

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(Guid id) => Ok(await _serviceManager.OrderService.GetAsync(id));

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods() => Ok(await _serviceManager.OrderService.GetAllDeliveryMethodAsync());
    }
}
