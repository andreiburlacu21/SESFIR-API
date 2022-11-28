using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Services.Model.Service.Contracts
{
    public interface IServiceAccounts : IService<AccountDTO>
    {
        Task<AccountDTO> SearchByUserNameAsync(string userName);
        Task<AccountDTO> SearchByEmailAsync(string email);
    }
}
