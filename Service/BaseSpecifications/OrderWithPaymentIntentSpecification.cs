using Domain.Models.Orders;
using Service.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseSpecifications
{
    internal class OrderWithPaymentIntentSpecification(string paymentIntentId)
        :BaseSpecifications<Order>(Order => Order.PaymentIntentId == paymentIntentId)
    {
    }
}
