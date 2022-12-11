using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DTOs
{
    public class ReviewWithEntitiesDTO : ReviewDTO
    {
        public AccountDTO Account { get; set; }
        public LocationDTO Location { get; set; }
    }
}
