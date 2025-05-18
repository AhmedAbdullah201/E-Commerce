using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Orders
{
    public class Order
        : BaseEntity<Guid>
    {
        public Order()
        {
        }

        public Order(string emailAdress,
                     IEnumerable<OrderItem> orderItems,
                     OrderAddress orderAddress,
                     DeliveryMethod deliveryMethods,
                     decimal subtotoal,
                     string paymentIntent)
        {
            BuyerEmail = emailAdress;
            OrderItems = orderItems;
            ShipToAddress = orderAddress;
            DeliveryMethod = deliveryMethods;
            Subtotoal = subtotoal;
            PaymentIntentId = paymentIntent;
        }

        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public IEnumerable<OrderItem> OrderItems { get; set; } = [];
        public OrderAddress ShipToAddress { get; set; } = default!;
        public DeliveryMethod   DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string PaymentIntentId { get; set; } = default!;
        public decimal Subtotoal {  get; set; } 

    }
}
