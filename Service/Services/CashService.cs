using Domain.Contracts;
using ServiceAbstraction.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Services
{
    internal class CashService(ICashRepository _cashRepository)
        : ICashService
    {
        public async Task<string?> GetCashAsync(string cashKey)
        =>await _cashRepository.GetCashAsync(cashKey);

        public async Task SetCashAsync(string cashKey, object cashValue, TimeSpan expirationTime)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(cashValue, options);
            await _cashRepository.SetCashAsync(cashKey, json, expirationTime);
        }
    }
}
