namespace Domain.Models.Product
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
