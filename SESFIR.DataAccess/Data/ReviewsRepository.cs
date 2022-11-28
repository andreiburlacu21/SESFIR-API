using SESFIR.DataAccess.ConnectionAccess;
using SESFIR.DataAccess.Data.AbstractRepository;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Data.Interfaces;

namespace SESFIR.DataAccess.Data
{
    public sealed class ReviewsRepository : Repository<Review>, IReviewsRepository
    {
        public ReviewsRepository(ISQLDataAccess sqlDataAccess) : base(sqlDataAccess)
        {
        }
    }
}