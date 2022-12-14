using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Services.Model.Service.Contracts
{
    public interface IServiceBookings : IService<BookingDTO>
    {
        Task<BookingWithEntitiesDTO> GetBookingEnitityAsync(int id);
        Task<IEnumerable<string>> GetCurrentDates();
        Task<bool> CheckDateAvailability(string date);
        Task<List<BookingDTO>> GetBookingsByLocationIdAsync(int locationId);
    }
}
