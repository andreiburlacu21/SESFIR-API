using SESFIR.DataAccess.ConnectionAccess;
using SESFIR.DataAccess.Data.AbstractRepository;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Data.Interfaces;

namespace SESFIR.DataAccess.Data
{
    public sealed class AccountsRepository : Repository<Accounts>, IAccountsRepository
    {
        public AccountsRepository(ISQLDataAccess sqlDataAccess) : base(sqlDataAccess)
        {
        }
    }
}
