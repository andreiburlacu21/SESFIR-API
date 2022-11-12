﻿using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Services.Model.Service.Contracts
{
    public interface IServiceAccounts : IService<AccountsDTO>
    {
        Task<AccountsDTO> SearchByUserNameAsync(string userName);
        Task<AccountsDTO> SearchByEmailAsync(string email);
    }
}