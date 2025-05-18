using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.IService
{
    public interface ICashService
    {
        Task<string?> GetCashAsync(string cashKey);
        Task SetCashAsync(string cashKey, object cashValue, TimeSpan expirationTime);
    }
}
