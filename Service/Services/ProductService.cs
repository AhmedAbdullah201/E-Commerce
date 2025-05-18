using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Product;
using Service.BaseSpecifications;
using Service.Specifications;
using ServiceAbstraction.IService;
using Shared;
using Shared.Dtos.ProductDto;
using Shared.Dtos.Products;

namespace Service.Services
{
    internal class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper)
        : IProductService
    {
        public async Task<ProductResponseDto> GetProductAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(specifications) 
                ?? throw new ProductNotFoundException(id);
            return _mapper.Map<ProductResponseDto>(product); ;
        }

        public async Task<PaginatedResult<ProductResponseDto>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(parameters);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            var data = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            var pageCount = data.Count();
            var totalCount = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(new ProductsCountSpecifications(parameters));
            return new  (parameters.PageIndex,pageCount,totalCount,data);
        }
        public async Task<IEnumerable<BrandResponseDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(false);
            return _mapper.Map<IEnumerable<BrandResponseDto>>(brands);
        }

        public async Task<IEnumerable<TypeResponseDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(false);
            return _mapper.Map<IEnumerable<TypeResponseDto>>(Types);
        }
    }
}
