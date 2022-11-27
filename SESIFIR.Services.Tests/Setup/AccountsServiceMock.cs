using AutoMapper;
using FluentValidation;
using Moq;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Mappers;
using SESFIR.Validators.Model.Validation;

namespace SESIFIR.Services.Tests.Setup;

internal class AccountsServiceMock
{
    #region Fields
    private Mock<ISQLDataFactory> mockRepositories;
    private IMapper mockMapper;
    private IValidator<AccountsDTO> mockValidator;
    private List<Accounts> mockAccounts;
    #endregion

    #region Contructors
    public AccountsServiceMock()
    {
        this.mockRepositories = new Mock<ISQLDataFactory>();

        this.mockValidator = new AccountsValidation();

        this.mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new SQLDatabaseProfile()); 

        }).CreateMapper();

        this.SetUpMockAccounts();
    }
    #endregion

    #region Properties
    public ISQLDataFactory Service { get => mockRepositories.Object; }
    public IMapper Mapper 
    { 
        get 
        {
            return mockMapper;
        } 
    }
    public IValidator<AccountsDTO> Validator 
    {
        get
        {
            return mockValidator;
        }
    }
    #endregion

    #region Methods
    public void SetUpInsert(AccountsDTO accountDTO)
    {
        mockRepositories.Setup(x => x.AccountsRepository.InsertAsync(It.IsAny<Accounts>()))
            .ReturnsAsync(() =>
            {
                var account = this.Mapper.Map<Accounts>(accountDTO);

                var count = mockAccounts.Count + 1;

                account.AccountId = count;

                mockAccounts.Add(account);

                return mockAccounts.FirstOrDefault(x => x.AccountId == account.AccountId);
            });
    }
    public void SetUpUpdate(AccountsDTO accountDTO)
    {
        mockRepositories.Setup(x => x.AccountsRepository.UpdateAsync(It.IsAny<Accounts>()))
            .ReturnsAsync(() =>
            {
                var account = this.Mapper.Map<Accounts>(accountDTO);

                mockAccounts.RemoveAll(x => x.AccountId == account.AccountId);

                mockAccounts.Add(account);

                return mockAccounts.FirstOrDefault(x => x.AccountId == account.AccountId);
            });
    }
    public void SetUpGetAll()
    {
        mockRepositories.Setup(x => x.AccountsRepository.GetAllAsync())
            .ReturnsAsync(() =>
            {
                return mockAccounts;
            });
    }
    public void SetUpFirstOrDefault(Func<Accounts,bool> expresion)
    {
        mockRepositories.Setup(x => x.AccountsRepository.FirstOrDefaultAsync(It.IsAny<Func<Accounts, bool>>()))
           .ReturnsAsync(() =>
           {
               return mockAccounts.FirstOrDefault(expresion);
           });
    }
    public void SetUpSearchByID(int id)
    {
        mockRepositories.Setup(x => x.AccountsRepository.SearchByIdAsync(id))
            .ReturnsAsync(() =>
            {
                return mockAccounts.FirstOrDefault(x => x.AccountId == id);
            });
    }
    private void SetUpMockAccounts()
    {
        mockAccounts = new List<Accounts>()
        {
            new Accounts()
            {
                AccountId = 1,
                PhoneNumber = "1234567890",
                Email= "1234@yahoo.com"
            },
            new Accounts()
            {
                AccountId = 0,
                UserName = "Test",
                Email = "wewe@yahoo.com",
                Password = "23123",
                PhoneNumber= "1234567890",
                Role = SESFIR.Utils.Enums.Role.User
            }
        };
    }
    #endregion
}
