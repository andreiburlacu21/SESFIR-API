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
        private readonly IValidator<BookingsDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BookingsService(ISQLDataFactory repositories, IValidator<BookingsDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(BookingsDTO value)
        {
            var Bookings = _mapper.Map<Bookings>(value);

            return await _repositories.BookingsRepository.DeleteAsync(Bookings);
        }

        public async Task<List<BookingsDTO>> GetAllAsync()
        {
            var Bookings = await _repositories.BookingsRepository.GetAllAsync();

            if (!Bookings.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<BookingsDTO>>(Bookings);
        }

        public async Task<BookingsDTO> InsertAsync(BookingsDTO value)
        {
            if (await _repositories.BookingsRepository.FirstOrDefaultAsync(x => x.LocationId == value.LocationId && 
                                                                                x.InDate == value.InDate) is not null)
                throw new ValidationException("Booking already exists");

            await Validate.FluentValidate(_validator, value);

            var userDTO = await _repositories.BookingsRepository.InsertAsync(_mapper.Map<Bookings>(value));

            return _mapper.Map<BookingsDTO>(userDTO);
        }

        public async Task<BookingsDTO> SearchByIdAsync(int id)
        {
            var booking = await _repositories.BookingsRepository.SearchByIdAsync(id);

            return _mapper.Map<BookingsDTO>(booking);
        }

        public async Task<BookingsDTO> UpdateAsync(BookingsDTO value)
        {
            if (await _repositories.BookingsRepository.SearchByIdAsync(value.BookingId) is null)
                throw new ValidationException("Booking does not exists");    

            await Validate.FluentValidate(_validator, value);

            var booking = await _repositories.BookingsRepository.UpdateAsync(_mapper.Map<Bookings>(value));

            return _mapper.Map<BookingsDTO>(booking);
        }
        #endregion
    }
}
