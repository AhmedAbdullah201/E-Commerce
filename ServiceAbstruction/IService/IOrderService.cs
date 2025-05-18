
using Shared.Dtos.Orders;

namespace ServiceAbstraction.IService
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(OrderRequest request, string Email);
        Task<OrderResponse> GetAsync(Guid Id);
        Task<IEnumerable<OrderResponse>> GetAllAsync(string Email);
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodAsync();
    }
}
