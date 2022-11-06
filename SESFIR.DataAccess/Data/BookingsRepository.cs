using SESFIR.DataAccess.ConnectionAccess;
using SESFIR.DataAccess.Data.AbstractRepository;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Data.Interfaces;

namespace SESFIR.DataAccess.Data
{
    public sealed class BookingsRepository : Repository<Bookings>, IBookingsRepository
    {
        public BookingsRepository(ISQLDataAccess sqlDataAccess) : base(sqlDataAccess)
        {
        }
    }
}