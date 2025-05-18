using Domain.Models.Product;
using Service.Specifications;
using Shared.Dtos;
using Shared.Dtos.ProductDto;

namespace Service.BaseSpecifications
{
    public class ProductWithBrandAndTypeSpecifications: BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id) 
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(ProductQueryParameters parameters)
            : base(CreateCriteria(parameters))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
                
            ApplyPagination(parameters.PageSize, parameters.PageIndex);

            if (parameters.SortOption is not null)
                ApplySort(parameters);

        }

        private static System.Linq.Expressions.Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return product =>
                        (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value)
                        && (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value)
                        &&(string.IsNullOrWhiteSpace(parameters.Search)||product.Name.ToLower().Trim().Contains(parameters.Search.ToLower().Trim()));
        }

        private void ApplySort(ProductQueryParameters parameters)
        {
            switch (parameters.SortOption)
            {
                case ProductSortOption.PriceDesc:
                    SetByDesc(product => product.Price);
                    break;
                case ProductSortOption.PriceAsc:
                    SetOrderBy(product => product.Price);
                    break;
                case ProductSortOption.NameDesc:
                    SetByDesc(product => product.Name);
                    break;
                case ProductSortOption.NameAsc:
                    SetOrderBy(product => product.Name);
                    break;
                default:
                    break;
            }

        }
    }
}
