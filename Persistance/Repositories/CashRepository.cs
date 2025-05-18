using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class CashRepository(IConnectionMultiplexer context)
        : ICashRepository
    {
        private readonly IDatabase _database = context.GetDatabase();
        public async Task<string?> GetCashAsync(string cashKey)
        {
            var result = await  _database.StringGetAsync(cashKey);
            return result.IsNullOrEmpty ? null : result.ToString();
        }

        public async Task SetCashAsync(string cashKey, string cashValue, TimeSpan expirationTime)
        => await _database.StringSetAsync(cashKey, cashValue, expirationTime);
    }
}
