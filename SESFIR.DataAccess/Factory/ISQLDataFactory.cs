using SESFIR.DataAccess.Data.Interfaces;

namespace SESFIR.DataAccess.Factory
{
    public interface ISQLDataFactory
    {
        IAccountsRepository AccountsRepository { get; }
        IBookingsRepository BookingsRepository { get; }
        ILocationsRepository LocationsRepository { get; }
        IReviewsRepository ReviewsRepository { get; }
    }
}