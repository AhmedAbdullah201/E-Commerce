using Domain.Contracts;
using Domain.Models.Product;
using Shared;
using Shared.Dtos.ProductDto;

namespace Service.Specifications
{
    public class ProductsCountSpecifications : BaseSpecifications<Product>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters)
           : base(CreateCriteria(parameters))
        {

        }
        private static System.Linq.Expressions.Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return product =>
                        (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value)
                        && (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value)
                        && (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Trim().Contains(parameters.Search.ToLower().Trim()));
        }

    }
}
