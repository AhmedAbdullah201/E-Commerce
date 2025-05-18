using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public ProductBrand ProductBrand { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public ProductType ProductType { get; set; }

    }
}
