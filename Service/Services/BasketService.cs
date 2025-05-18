using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Baskets;
using ServiceAbstraction.IService;
using Shared.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    internal class BasketService( IBasketRepsitory _basketRepsitory,IMapper _mapper)
                : IBasketService
    {
        public async Task<bool> DeleteAsync(string id)
            => await _basketRepsitory.DeleteAsync(id);

        public async Task<BasketDto> GetAsync(string id)
        {
           var basket = await _basketRepsitory.GetAsync(id) ??
                throw new BasketNotFoundException(id);

            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> UpdateAsync(BasketDto dto)
        {
            var customerBasket = _mapper.Map<CustomerBasket >(dto);
            var updatedBasket = await _basketRepsitory.UpdateAsync(customerBasket) ??
                throw new Exception("can`t update basket right now !");

            return _mapper.Map<BasketDto>(updatedBasket);
        }
    }
}
