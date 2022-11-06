using Dapper.Contrib.Extensions;

namespace SESFIR.DataAccess.Data.Domains
{
    [Table("table_Bookings")]
    public sealed class Bookings
    {
        public int BookingId { get; set; }
        public int AccountId { get; set; }
        public int LocationId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InDate { get; set; }
        public string? OutDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
