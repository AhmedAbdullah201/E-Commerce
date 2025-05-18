using Domain.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        : IBasketRepsitory
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
         public async Task<CustomerBasket?> GetAsync(string id)
         {
             var basket  =await _database.StringGetAsync(id);

            if(basket.IsNullOrEmpty) 
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(basket!);
         }
        public async Task<bool> DeleteAsync(string id) => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket =  JsonSerializer.Serialize(basket);

            var  isCreatedOrUpdated = await _database.StringSetAsync(basket.Id,jsonBasket,timeToLive ?? TimeSpan.FromDays(7));

            return isCreatedOrUpdated ? await GetAsync(basket.Id) : null;
        }
    }
}
