using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int AccountId { get; set; }
        public int LocationId { get; set; }
        public int Grade { get; set; }
        public string? Description { get; set; }
        public string? Date { get; set; }
    }
}
