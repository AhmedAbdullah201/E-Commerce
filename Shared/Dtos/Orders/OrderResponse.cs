using Shared.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Orders
{
    public record OrderResponse
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } 
        public AddressDto ShipToAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string DeliveryCost { get; set; } = default!;
        public IEnumerable<OrderItemDto> Items { get; set; } = [];
        public string Status { get; set; } = default!;
        public string PaymentIntent { get; set; } = string.Empty;
        public decimal Subtotoal { get; set; }
        public decimal Totoal { get; set; }
    };
}
