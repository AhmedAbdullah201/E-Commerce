namespace Shared.Dtos.Orders
{
    public record OrderItemDto
    {
        public int ProductId { get; set; }
        public string  ProductName{ get; set; } = default!;
        public string  PictureUrl{ get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
