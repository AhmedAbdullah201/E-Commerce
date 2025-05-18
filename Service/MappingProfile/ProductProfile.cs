using AutoMapper;
using Domain.Models.Product;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.Products;

namespace Service.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandResponseDto>();
            CreateMap<ProductType, TypeResponseDto>();
        }

    }
}
internal class PictureUrlResolver(IConfiguration _configrations)
    : IValueResolver<Product, ProductResponseDto, string>
{
    public string Resolve(Product source, ProductResponseDto destination, string destMember, ResolutionContext context) 
        => string.IsNullOrWhiteSpace(source.PictureUrl) ? string.Empty : $"{_configrations["BaseUrl"]}{source.PictureUrl}";
}
