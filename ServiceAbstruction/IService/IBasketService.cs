using Shared.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.IService
{
    public interface IBasketService
    {
        Task<BasketDto> GetAsync(string id);
        Task<BasketDto> UpdateAsync(BasketDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
