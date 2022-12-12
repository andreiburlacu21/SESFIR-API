using FluentValidation;
using NUnit.Framework;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service;
using SESFIR.Utils.Enums;
using SESIFIR.Services.Tests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESIFIR.Services.Tests;

public class BookingServiceTests
{
    private BookingsService bookingsService;
    private BookingsServiceMock bookingsServiceMock;

    [SetUp]
    public void Initialize()
    {
        this.bookingsServiceMock = new BookingsServiceMock();

        this.bookingsService = new BookingsService(bookingsServiceMock.Service, bookingsServiceMock.Validator, bookingsServiceMock.Mapper);
    }

    [Test]
    public async Task Should_SearchyById_and_Return_ExpectedValue()
    {
        // arrange
        var bookingDTO = new BookingDTO()
        {
            BookingId = 3,
            PhoneNumber = "1234567890",
            AccountId = 3,
            LocationId = 3,
            InDate = "12-12-2022",
            OutDate = "12-13-2022",
            TotalPrice = 123

        };
        this.bookingsServiceMock.SetUpSearchByID(bookingDTO.BookingId);

        // act
        var Booking = await bookingsService.SearchByIdAsync(bookingDTO.BookingId);

        // assert
        Assert.That(Booking, Is.Not.Null);

    }

    [Test]
    public async Task Should_SearchyById_and_Return_NotFound()
    {
        // arrange
        this.bookingsServiceMock.SetUpSearchByID(33);

        // act
        var Booking = await bookingsService.SearchByIdAsync(33);

        // assert
        Assert.That(Booking, Is.Null);
    }

    [Test]
    public async Task Should_Insert_and_Return_ExpectedValue()
    {
        // arrange
        var bookingDTO = new BookingDTO()
        {
            BookingId = 3,
            PhoneNumber = "1234567890",
            AccountId = 3,
            LocationId = 3,
            InDate = "12-12-2022",
            OutDate = "12-13-2022",
            TotalPrice = 123

        };
        this.bookingsServiceMock.SetUpSearchByID(bookingDTO.BookingId);
        this.bookingsServiceMock.SetUpGetAll();
        this.bookingsServiceMock.SetUpInsert(bookingDTO);

        // act
        var Booking = await bookingsService.InsertAsync(bookingDTO);
        var Bookings = await bookingsService.GetAllAsync();

        // assert
        Assert.IsNotNull(Booking);
        Assert.IsTrue(Bookings.Any(x => x.BookingId == Booking.BookingId));
    }

    [Test]
    public async Task Should_Insert_and_Throw_ValdiationException()
    {
        // arrange
        var bookingDTO = new BookingDTO()
        {
            BookingId = 3,
            PhoneNumber = "",
            AccountId = 3,
            LocationId = 3,
            InDate = "12-12-2022",
            OutDate = "12-13-2022",
            TotalPrice = 123

        };
        this.bookingsServiceMock.SetUpSearchByID(bookingDTO.BookingId);
        this.bookingsServiceMock.SetUpGetAll();
        this.bookingsServiceMock.SetUpInsert(bookingDTO);
        try
        {
            // act
            await bookingsService.InsertAsync(bookingDTO);

            await bookingsService.GetAllAsync();

            Assert.Fail();
        }
        catch (ValidationException ex)
        {
            // assert

            Assert.That(ex, Is.Not.Null);

            Assert.That(ex.Message, Is.Not.Empty);
        }

    }

    [Test]
    public async Task Should_Update_and_Return_ExpectedValue()
    {
        // arrange
        var bookingDTO = new BookingDTO()
        {
            BookingId = 3,
            PhoneNumber = "1234567891",
            AccountId = 3,
            LocationId = 3,
            InDate = "12-12-2022",
            OutDate = "12-13-2022",
            TotalPrice = 123

        };
        this.bookingsServiceMock.SetUpSearchByID(bookingDTO.BookingId);
        this.bookingsServiceMock.SetUpGetAll();
        this.bookingsServiceMock.SetUpUpdate(bookingDTO);
        this.bookingsServiceMock.SetUpFirstOrDefault(x => x.PhoneNumber == bookingDTO.PhoneNumber);


        // act
        var initialBooking = await bookingsService.SearchByIdAsync(bookingDTO.BookingId);

        var updatedBooking = await bookingsService.UpdateAsync(bookingDTO);

        // assert
        Assert.IsNotNull(initialBooking);
        Assert.IsNotNull(updatedBooking);

        Assert.IsTrue(initialBooking.BookingId == updatedBooking.BookingId);

        //this test only changes the password
        Assert.AreNotSame(initialBooking.PhoneNumber, updatedBooking.PhoneNumber);
    }

    [Test]
    public async Task Should_Update_and_Throw_ValdiationException()
    {
        // arrange
        var bookingDTO = new BookingDTO()
        {
            BookingId = 3,
            PhoneNumber = "",
            AccountId = 3,
            LocationId = 3,
            InDate = "12-12-2022",
            OutDate = "12-13-2022",
            TotalPrice = 123

        };
        this.bookingsServiceMock.SetUpSearchByID(bookingDTO.BookingId);
        this.bookingsServiceMock.SetUpGetAll();
        this.bookingsServiceMock.SetUpUpdate(bookingDTO);
        try
        {
            // act
            await bookingsService.UpdateAsync(bookingDTO);

            await bookingsService.GetAllAsync();

            Assert.Fail();
        }
        catch (ValidationException ex)
        {
            // assert

            Assert.That(ex, Is.Not.Null);

            Assert.That(ex.Message, Is.Not.Empty);
        }

    }
}
