using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Orders;
using Domain.Models.Product;
using Microsoft.Extensions.Configuration;
using Service.BaseSpecifications;
using ServiceAbstraction.IService;
using Shared.Dtos.Basket;
using Stripe;


namespace Service.Services
{
    internal class PaymentService(IUnitOfWork _unitOfWork, IMapper _mapper ,IConfiguration _configuration ,IBasketRepsitory _basketRepsitory)
        : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetRequiredSection("StripeSettings")["Secretkey"];
            
            var basket =await _basketRepsitory.GetAsync(basketId)??
                throw new BasketNotFoundException(basketId);

            var productRepository =  _unitOfWork.GetRepository<Domain.Models.Product.Product, int>();
            foreach (var item in basket.Items)
            {
                var product =await productRepository.GetAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                item.ProductName = product.Name;
                item.Price = product.Price;
            }

            ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value) ??
                throw new DeliveryNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Cost;

            var amount = (long)(basket.Items.Sum(item => item.Quantity * item.Price) + basket.ShippingPrice) * 100;

            var paymentService = new PaymentIntentService();
            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))//  Create 
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "AED",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await paymentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await paymentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepsitory.UpdateAsync(basket);

            return _mapper.Map<BasketDto>(basket);  
        }

        public async Task UpdateOrderPaymentStatusAsync(string jsonRequest, string stripeHeader)
        {
            var endpointSecret = _configuration.GetRequiredSection("Stripe")["EndpointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,stripeHeader, endpointSecret);

            var paymentIntentId = stripeEvent.Data.Object as PaymentIntent;

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFailedAsync(paymentIntentId.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentReceivedAsync(paymentIntentId.Id); 
                    break;
                default:
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                    break;
            }
        }
        private async Task UpdatePaymentReceivedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));

            order!.Status = PaymentStatus.PaymentReceived;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);

            await _unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));

            order.Status = PaymentStatus.PaymentFailed;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
