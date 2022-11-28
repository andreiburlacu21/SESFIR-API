using SESFIR.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DTOs
{
    public sealed class AccountDTO
    {
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; }
    }
}
