using AutoMapper;
using FluentValidation;
using Moq;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Mappers;
using SESFIR.Utils.Enums;
using SESFIR.Validators.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESIFIR.Services.Tests.Setup
{
    internal class BookingsServiceMock
    {
        #region Fields
        private Mock<ISQLDataFactory> mockRepositories;
        private IMapper mockMapper;
        private IValidator<BookingDTO> mockValidator;
        private List<Booking> mockBookings;
        #endregion

        #region Contructors
        public BookingsServiceMock()
        {
            this.mockRepositories = new Mock<ISQLDataFactory>();

            this.mockValidator = new BookingsValidation();

            this.mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SQLDatabaseProfile());

            }).CreateMapper();

            this.SetUpMockBookings();
        }
        #endregion

        #region Properties
        public ISQLDataFactory Service { get => mockRepositories.Object; }
        public IMapper Mapper
        {
            get
            {
                return mockMapper;
            }
        }
        public IValidator<BookingDTO> Validator
        {
            get
            {
                return mockValidator;
            }
        }
        #endregion

        #region Methods
        public void SetUpInsert(BookingDTO accountDTO)
        {
            mockRepositories.Setup(x => x.BookingsRepository.InsertAsync(It.IsAny<Booking>()))
                .ReturnsAsync(() =>
                {
                    var account = this.Mapper.Map<Booking>(accountDTO);

                    var count = mockBookings.Count + 1;

                    account.BookingId = count;

                    mockBookings.Add(account);

                    return mockBookings.FirstOrDefault(x => x.BookingId == account.BookingId);
                });
        }
        public void SetUpUpdate(BookingDTO accountDTO)
        {
            mockRepositories.Setup(x => x.BookingsRepository.UpdateAsync(It.IsAny<Booking>()))
                .ReturnsAsync(() =>
                {
                    var account = this.Mapper.Map<Booking>(accountDTO);

                    mockBookings.RemoveAll(x => x.BookingId == account.BookingId);

                    mockBookings.Add(account);

                    return mockBookings.FirstOrDefault(x => x.BookingId == account.BookingId);
                });
        }
        public void SetUpGetAll()
        {
            mockRepositories.Setup(x => x.BookingsRepository.GetAllAsync())
                .ReturnsAsync(() =>
                {
                    return mockBookings;
                });
        }
        public void SetUpFirstOrDefault(Func<Booking, bool> expresion)
        {
            mockRepositories.Setup(x => x.BookingsRepository.FirstOrDefaultAsync(It.IsAny<Func<Booking, bool>>()))
               .ReturnsAsync(() =>
               {
                   return mockBookings.FirstOrDefault(expresion);
               });
        }
        public void SetUpSearchByID(int id)
        {
            mockRepositories.Setup(x => x.BookingsRepository.SearchByIdAsync(id))
                .ReturnsAsync(() =>
                {
                    return mockBookings.FirstOrDefault(x => x.BookingId == id);
                });
        }
        private void SetUpMockBookings()
        {
            mockBookings = new List<Booking>()
        {
            new Booking()
            {
                BookingId = 1,
                PhoneNumber = "1234567890",
                AccountId = 1,
                LocationId= 1,
                InDate = "12-12-2022",
                OutDate =  "12-13-2022",
                TotalPrice = 123
            },
            new Booking()
            {
                BookingId = 2,
                PhoneNumber = "1234567890",
                AccountId = 2,
                LocationId= 2,
                InDate = "12-12-2022",
                OutDate =  "12-13-2022",
                TotalPrice = 123
            },
            new Booking()
            {
                BookingId = 3,
                PhoneNumber = "1234567890",
                AccountId = 3,
                LocationId= 3,
                InDate = "12-12-2022",
                OutDate =  "12-13-2022",
                TotalPrice = 123
            }
        };
        }
        #endregion
    }
}
