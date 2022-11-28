using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Services.Authentication.Service.Contracts;

public interface IAuthenticationService
{
    Task<dynamic> GenerateTokenAsync(AuthDTO authData);
    Task<bool> IsValidUserNameAndPassowrdAsync(AuthDTO authData);
    Task<AccountDTO> RegisterAsync(AccountDTO user);
}
