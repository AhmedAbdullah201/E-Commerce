using Shared.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.IService
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntent(string basketId); 
        Task UpdateOrderPaymentStatusAsync(string jsonRequest , string stripeHeader);
    }
}
