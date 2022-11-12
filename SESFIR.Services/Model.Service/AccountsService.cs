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
        private readonly IValidator<AccountsDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AccountsService(ISQLDataFactory repositories, IValidator<AccountsDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(AccountsDTO value)
        {
            var accounts = _mapper.Map<Accounts>(value);

            return await _repositories.AccountsRepository.DeleteAsync(accounts);
        }

        public async Task<List<AccountsDTO>> GetAllAsync()
        {
            var accounts = await _repositories.AccountsRepository.GetAllAsync();

            if (!accounts.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<AccountsDTO>>(accounts);
        }

        public async Task<AccountsDTO> InsertAsync(AccountsDTO value)
        {
            if (await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == value.UserName) is not null)
                throw new ValidationException("Account already exists");

            await Validate.FluentValidate(_validator, value);

            var accountDTO = await _repositories.AccountsRepository.InsertAsync(_mapper.Map<Accounts>(value));

            return _mapper.Map<AccountsDTO>(accountDTO);
        }

        public async Task<AccountsDTO> SearchByIdAsync(int id)
        {
            var accountsDTO = await _repositories.AccountsRepository.SearchByIdAsync(id);

            return _mapper.Map<AccountsDTO>(accountsDTO);
        }

        public async Task<AccountsDTO> UpdateAsync(AccountsDTO value)
        {
            if (await _repositories.AccountsRepository.SearchByIdAsync(value.AccountId) is null)
                throw new ValidationException("Account does not exists");

            var accountsSearch = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == value.UserName);

            if (accountsSearch is not null && value.AccountId != accountsSearch.AccountId)
                throw new ValidationException("This Username is taken");

            await Validate.FluentValidate(_validator, value);

            var account = await _repositories.AccountsRepository.UpdateAsync(_mapper.Map<Accounts>(value));

            return _mapper.Map<AccountsDTO>(account);
        }
        #endregion

        public async Task<AccountsDTO> SearchByEmailAsync(string email)
        {
            var accountsDTO = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.Email == email);

            return _mapper.Map<AccountsDTO>(accountsDTO);
        }

        public async Task<AccountsDTO> SearchByUserNameAsync(string userName)
        {
            var accountsDTO = await _repositories.AccountsRepository.FirstOrDefaultAsync(x => x.UserName == userName);

            return _mapper.Map<AccountsDTO>(accountsDTO);
        }
    }
}
