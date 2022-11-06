using Dapper.Contrib.Extensions;

namespace SESFIR.DataAccess.Data.Domains
{
    [Table("table_Accounts")]
    public sealed class Accounts
    {
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Role { get; set; }

    }
}
