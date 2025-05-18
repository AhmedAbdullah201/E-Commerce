using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServiceAbstraction.IService;
using Shared;
using Shared.Dtos.ProductDto;
using Shared.Dtos.Products;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager)
        : APIController
    {
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductAsync(int id)
        {
            var result = await _serviceManager.ProductService.GetProductAsync(id);
            return Ok(result);
        }
        [RedisCache]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResponseDto>>> GetAllProductsAsync([FromQuery] ProductQueryParameters parameters)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(result);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResponseDto>>> GetAllBrandsAsync()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(result);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResponseDto>>> GetAllTypesAsync()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(result);
        }
    }
}
