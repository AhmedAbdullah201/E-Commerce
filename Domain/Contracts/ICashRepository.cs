using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICashRepository
    {
        Task<string?> GetCashAsync(string cashKey);
        Task SetCashAsync(string cashKey, string cashValue , TimeSpan expirationTime);
    }
}
