using AutoMapper;
using Domain.Models.Orders;
using Domain.Models.Product;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.AuthenticationDto;
using Shared.Dtos.Orders;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryCost, opt => opt.MapFrom(src => src.DeliveryMethod.Cost))
                .ForMember(dest => dest.Totoal, opt => opt.MapFrom(src => src.DeliveryMethod.Cost + src.Subtotoal));
        }
    }
    internal class OrderItemPictureUrlResolver(IConfiguration _configrations)
    : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
            => string.IsNullOrWhiteSpace(source.Product.PictureUrl) ? string.Empty : $"{_configrations["BaseUrl"]}{source.Product.PictureUrl}";
    }
}
