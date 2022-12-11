using AutoMapper;
using FluentValidation;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;
using SESFIR.Validators;

namespace SESFIR.Services.Model.Service
{
    public sealed class BookingsService : IServiceBookings
    {
        #region Fields
        private readonly ISQLDataFactory _repositories;
        private readonly IValidator<BookingDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BookingsService(ISQLDataFactory repositories, IValidator<BookingDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(BookingDTO value)
        {
            var Bookings = _mapper.Map<Booking>(value);

            return await _repositories.BookingsRepository.DeleteAsync(Bookings);
        }

        public async Task<List<BookingDTO>> GetAllAsync()
        {
            var Bookings = await _repositories.BookingsRepository.GetAllAsync();

           // if (!Bookings.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<BookingDTO>>(Bookings);
        }

        public async Task<List<BookingDTO>> GetBookingsByLocationIdAsync(int locationId)
        {
            var Bookings = await _repositories.BookingsRepository.GetEntitiesWhereAsync(x => x.LocationId == locationId);

            return _mapper.Map<List<BookingDTO>>(Bookings);
        }


        public async Task<BookingDTO> InsertAsync(BookingDTO value)
        {
            if (await _repositories.BookingsRepository.FirstOrDefaultAsync(x => x.LocationId == value.LocationId && 
                                                                                x.InDate == value.InDate) is not null)
                throw new ValidationException("Booking already exists");

            await Validate.FluentValidate(_validator, value);

            var userDTO = await _repositories.BookingsRepository.InsertAsync(_mapper.Map<Booking>(value));

            return _mapper.Map<BookingDTO>(userDTO);
        }

        public async Task<BookingDTO> SearchByIdAsync(int id)
        {
            var booking = await _repositories.BookingsRepository.SearchByIdAsync(id);

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<BookingDTO> UpdateAsync(BookingDTO value)
        {
            if (await _repositories.BookingsRepository.SearchByIdAsync(value.BookingId) is null)
                throw new ValidationException("Booking does not exists");    

            await Validate.FluentValidate(_validator, value);

            var booking = await _repositories.BookingsRepository.UpdateAsync(_mapper.Map<Booking>(value));

            return _mapper.Map<BookingDTO>(booking);
        }
        #endregion


        public async Task<BookingWithEntitiesDTO> GetBookingEnitityAsync(int id)
        {
            var booking = await _repositories.BookingsRepository.SearchByIdAsync(id);

            var account = await _repositories.AccountsRepository.SearchByIdAsync(booking.AccountId);

            var location = await _repositories.LocationsRepository.SearchByIdAsync(booking.LocationId);

            var bookingWithEntitiesDTO = _mapper.Map<BookingWithEntitiesDTO>(booking);

            bookingWithEntitiesDTO.Account = _mapper.Map<AccountDTO>(account);

            bookingWithEntitiesDTO.Location = _mapper.Map<LocationDTO>(location);

            return bookingWithEntitiesDTO;
        }


        public async Task<IEnumerable<string>> GetCurrentDates()
        {
            return (await _repositories.BookingsRepository.GetEntitiesWhereAsync(x => x.InDate != ""))
                                                          .Select(x => x.InDate) ?? Enumerable.Empty<string>();
        }

        public async Task<bool> CheckDateAvailability(string searchDate)
        {
            var date = await _repositories.BookingsRepository.FirstOrDefaultAsync(x => x.InDate == searchDate);

            if (date != null)
                return false;

            return true;
        }
          


    }

}
