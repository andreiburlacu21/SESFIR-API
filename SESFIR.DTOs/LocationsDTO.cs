using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DTOs
{
    public sealed class LocationsDTO
    {
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? ImageLocation { get; set; }
        public double PricePerHour { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
    }
}
