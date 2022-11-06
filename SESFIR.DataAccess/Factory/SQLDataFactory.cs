using SESFIR.DataAccess.ConnectionAccess;
using SESFIR.DataAccess.Data;
using SESFIR.DataAccess.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DataAccess.Factory
{
    public sealed class SQLDataFactory : ISQLDataFactory
    {
        private readonly ISQLDataAccess _SQLDataAccess;
        public IAccountsRepository AccountsRepository => new AccountsRepository(_SQLDataAccess);
        public IBookingsRepository BookingsRepository => new BookingsRepository(_SQLDataAccess);
        public IReviewsRepository ReviewsRepository => new ReviewsRepository(_SQLDataAccess);
        public ILocationsRepository LocationsRepository => new LocationsRepository(_SQLDataAccess);
        public SQLDataFactory(ISQLDataAccess sQLDataAccess)
        {
            _SQLDataAccess = sQLDataAccess;
        }
    }
}
