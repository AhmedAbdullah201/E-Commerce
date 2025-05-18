namespace Domain.Models.Orders
{
    public class OrderAddress
        : BaseEntity<int>
    {
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
