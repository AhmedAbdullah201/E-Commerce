using Shared;
using Shared.Dtos.ProductDto;
using Shared.Dtos.Products;

namespace ServiceAbstraction.IService
{
    public interface IProductService
    {
        Task<ProductResponseDto> GetProductAsync(int id);
        Task<PaginatedResult<ProductResponseDto>> GetAllProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResponseDto>> GetAllTypesAsync();

    }
}
