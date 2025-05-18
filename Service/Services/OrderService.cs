using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Orders;
using Domain.Models.Product;
using Service.BaseSpecifications;
using ServiceAbstraction.IService;
using Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    internal class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper ,IBasketRepsitory _basketRepsitory)
        : IOrderService
    {
        public async Task<OrderResponse> CreateAsync(OrderRequest request, string email)
        {
            var basket =await _basketRepsitory.GetAsync(request.BasketId)??
                throw new BasketNotFoundException(request.BasketId);

            ArgumentNullException.ThrowIfNull(basket.PaymentIntentId);

            var orderRepo =  _unitOfWork.GetRepository<Order, Guid>();

            var orderExists =await orderRepo.GetAsync(new OrderWithPaymentIntentSpecification(basket.PaymentIntentId));

            if (orderExists != null) orderRepo.Delete(orderExists);

            var orderItems = new List<OrderItem>();
            var productrRepo =_unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket!.Items)
            {
                var product = await  productrRepo.GetAsync(item.Id)??
                    throw new  ProductNotFoundException(item.Id);
                var orderitem = new OrderItem(new(product.Id,product.Name,product.PictureUrl),product.Price,item.Quantity);

                orderItems.Add(orderitem);
                item.Price = product.Price;
            }

            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(request.DeliveryMethodId)??
                throw new DeliveryNotFoundException(request.DeliveryMethodId);

            var subtotal = orderItems.Sum(i=>i.Quantity *  i.Price);
            var order = new Order(email, orderItems,orderAddress, deliveryMethod, subtotal,basket.PaymentIntentId!);
            orderRepo.Add(order);
            await _unitOfWork.SaveChangesAsync();
    
            return _mapper.Map<OrderResponse>(order);
        }

        public Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
