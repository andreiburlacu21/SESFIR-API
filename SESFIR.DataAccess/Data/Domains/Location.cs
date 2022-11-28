using Dapper.Contrib.Extensions;
namespace SESFIR.DataAccess.Data.Domains
{
    [Table("table_Locations")]
    public sealed class Location
    {
        [ExplicitKey]
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? ImageLocation { get; set; }
        public double PricePerHour { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
    }
}
