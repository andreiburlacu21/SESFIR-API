using AutoMapper;
using FluentValidation;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;
using SESFIR.Validators;

namespace SESFIR.Services.Model.Service
{
    public sealed class AccountsService : IServiceAccounts
    {
        #region Fields
        private readonly ISQLDataFactory _repositories;
        private readonly IValidator<AccountDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AccountsService(ISQLDataFactory repositories, IValidator<AccountDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(AccountDTO value)
        {
            var accounts = _mapper.Map<Account>(value);

            return await _repositories.AccountsRepository.DeleteAsync(accounts);
        }

        public async Task<List<AccountDTO>> GetAllAsync()
        {
            var accounts = await _repositories.AccountsRepository.GetAllAsync();

           // if (!accounts.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<AccountDTO>>(accounts);
        }

        public async Task<AccountDTO> InsertAsync(AccountDTO value)
        {
            var acc = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName.Equals(value.UserName));

            if (acc is not null)
                throw new ValidationException("Account already exists");

            await Validate.FluentValidate(_validator, value);

            var account = _mapper.Map<Account>(value);

            account.Role = Utils.Enums.Role.User;

            var accountDTO = await _repositories.AccountsRepository.InsertAsync(account);

            return _mapper.Map<AccountDTO>(accountDTO);
        }

        public async Task<AccountDTO> SearchByIdAsync(int id)
        {
            var accountsDTO = await _repositories.AccountsRepository.SearchByIdAsync(id);

            return _mapper.Map<AccountDTO>(accountsDTO);
        }

        public async Task<AccountDTO> UpdateAsync(AccountDTO value)
        {
            if (await _repositories.AccountsRepository.SearchByIdAsync(value.AccountId) is null)
                throw new ValidationException("Account does not exists");

            var accountsSearch = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == value.UserName);

            if (accountsSearch is not null && value.AccountId != accountsSearch.AccountId)
                throw new ValidationException("This Username is taken");

            await Validate.FluentValidate(_validator, value);

            var account = await _repositories.AccountsRepository.UpdateAsync(_mapper.Map<Account>(value));

            return _mapper.Map<AccountDTO>(account);
        }
        #endregion

        public async Task<AccountDTO> SearchByEmailAsync(string email)
        {
            var accountsDTO = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.Email == email);

            return _mapper.Map<AccountDTO>(accountsDTO);
        }

        public async Task<AccountDTO> SearchByUserNameAsync(string userName)
        {
            var accountsDTO = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == userName);

            return _mapper.Map<AccountDTO>(accountsDTO);
        }
    }
}
