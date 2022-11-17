using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Services.Authentication.Service.Contracts;
using SESFIR.Validators.Model.Validation;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SESFIR.Validators;

namespace SESFIR.Services.Authentication.Service;
public class AuthenticationService : IAuthenticationService
{
    private readonly ISQLDataFactory _repositories;
    private readonly string _myKey;
    private readonly IMapper _mapper;

    public AuthenticationService(ISQLDataFactory repositories, string myKey, IMapper mapper)
    {
        _myKey = myKey;
        _repositories = repositories;
        _mapper = mapper;

    }
    private async Task<bool> CheckEmailAsync(string email)
    {
        return await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.Email == email) != null;

    }

    private async Task<bool> CheckUserNameAsync(string userName)
    {
        return await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == userName) != null;
    }

    public async Task<dynamic> GenerateTokenAsync(AuthDTO authData)
    {
        await IsValidUserNameAndPassowrdAsync(authData);
        var username = authData.UserName;
        var user = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == username);

        var claims = new List<Claim>
            {
                new Claim("Identifier", user.AccountId + ""),
                new Claim(ClaimTypes.Role, user.Role + ""),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())
            };

        var token = new JwtSecurityToken
            (
               new JwtHeader
                (
                    new SigningCredentials
                    (
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_myKey)),
                        SecurityAlgorithms.HmacSha256
                    )
                 ),
                new JwtPayload(claims)
            );
        var output = new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            user.UserName
        };

        return output;
    }


    public async Task<bool> IsValidUserNameAndPassowrdAsync(AuthDTO authData)
    {
        var user = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == authData.UserName) ??
                       throw new ValidationException("Invalid username or password");

        if (user.Password != authData.Password)
            throw new ValidationException("Invalid username or password");
        return true;
    }

    public async Task<AccountsDTO> RegisterAsync(AccountsDTO user)
    {
        if (await CheckEmailAsync(user.Email))
            throw new ValidationException("Invalid email");

        if (await CheckUserNameAsync(user.UserName))
            throw new ValidationException("Invalid username");

        user.Role = Utils.Enums.Role.User;

        var validator = new AccountsValidation();

        await Validate.FluentValidate(validator, user);

        var result = _mapper.Map<Accounts>(user);

        return _mapper.Map<AccountsDTO>(await _repositories.AccountsRepository.InsertAsync(result));

    }
}
