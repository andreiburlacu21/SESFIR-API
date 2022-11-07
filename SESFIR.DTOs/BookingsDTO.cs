using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DTOs
{
    public sealed class BookingsDTO
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
