using AutoMapper;
using Domain.Models.Identity;
using Shared.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
