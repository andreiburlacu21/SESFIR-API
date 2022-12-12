//using FluentValidation;
//using NUnit.Framework;
//using SESFIR.DTOs;
//using SESFIR.Services.Model.Service;
//using SESFIR.Utils.Enums;
//using SESIFIR.Services.Tests.Setup;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SESIFIR.Services.Tests;

//public class BookingServiceTests
//{
//    private BookingsService bookingsService;
//    private BookingsServiceMock bookingsServiceMock;

//    [SetUp]
//    public void Initialize()
//    {
//        this.bookingsServiceMock = new BookingsServiceMock();

//        this.bookingsService = new BookingsService(bookingsServiceMock.Service, bookingsServiceMock.Validator, bookingsServiceMock.Mapper);
//    }

//    [Test]
//    public async Task Should_SearchyById_and_Return_ExpectedValue()
//    {
//        arrange
//       var accountDTO = new BookingDTO()
//       {
//           BookingId = 3,
//           PhoneNumber = "1234567890",
//           AccountId = 3,
//           LocationId = 3,
//           InDate = "12-12-2022",
//           OutDate = "12-13-2022",
//           TotalPrice = 123

//       };
//        this.bookingsServiceMock.SetUpSearchByID(accountDTO.AccountId);

//        act
//       var account = await bookingsService.SearchByIdAsync(accountDTO.AccountId);

//        assert
//        Assert.That(account, Is.Not.Null);

//    }

//    [Test]
//    public async Task Should_SearchyById_and_Return_NotFound()
//    {
//        arrange
//        this.bookingsServiceMock.SetUpSearchByID(33);

//        act
//       var account = await bookingsService.SearchByIdAsync(33);

//        assert
//        Assert.That(account, Is.Null);
//    }

//    [Test]
//    public async Task Should_Insert_and_Return_ExpectedValue()
//    {
//        arrange
//       var accountDTO = new BookingDTO()
//       {
//           BookingId = 3,
//           PhoneNumber = "1234567890",
//           AccountId = 3,
//           LocationId = 3,
//           InDate = "12-12-2022",
//           OutDate = "12-13-2022",
//           TotalPrice = 123

//       };
//        this.bookingsServiceMock.SetUpSearchByID(accountDTO.AccountId);
//        this.bookingsServiceMock.SetUpGetAll();
//        this.bookingsServiceMock.SetUpInsert(accountDTO);

//        act
//       var account = await bookingsService.InsertAsync(accountDTO);
//        var accounts = await bookingsService.GetAllAsync();

//        assert
//        Assert.IsNotNull(account);
//        Assert.IsTrue(accounts.Any(x => x.AccountId == account.AccountId));
//    }

//    [Test]
//    public async Task Should_Insert_and_Throw_ValdiationException()
//    {
//        arrange
//       var accountDTO = new BookingDTO()
//       {
//           BookingId = 3,
//           PhoneNumber = "1234567890",
//           AccountId = 3,
//           LocationId = 3,
//           InDate = "12-12-2022",
//           OutDate = "12-13-2022",
//           TotalPrice = 123

//       };
//        this.bookingsServiceMock.SetUpSearchByID(accountDTO.AccountId);
//        this.bookingsServiceMock.SetUpGetAll();
//        this.bookingsServiceMock.SetUpInsert(accountDTO);
//        try
//        {
//            act
//           await bookingsService.InsertAsync(accountDTO);

//            await bookingsService.GetAllAsync();

//            Assert.Fail();
//        }
//        catch (ValidationException ex)
//        {
//            assert

//            Assert.That(ex, Is.Not.Null);

//            Assert.That(ex.Message, Is.Not.Empty);
//        }

//    }

//    [Test]
//    public async Task Should_Update_and_Return_ExpectedValue()
//    {
//        arrange
//       var accountDTO = new BookingDTO()
//       {
//           BookingId = 3,
//           PhoneNumber = "1234567890",
//           AccountId = 3,
//           LocationId = 3,
//           InDate = "12-12-2022",
//           OutDate = "12-13-2022",
//           TotalPrice = 123

//       };
//        this.bookingsServiceMock.SetUpSearchByID(accountDTO.AccountId);
//        this.bookingsServiceMock.SetUpGetAll();
//        this.bookingsServiceMock.SetUpUpdate(accountDTO);
//        this.bookingsServiceMock.SetUpFirstOrDefault(x => x.UserName == accountDTO.UserName);


//        act
//       var initialAccount = await bookingsService.SearchByIdAsync(accountDTO.AccountId);

//        var updatedAccount = await bookingsService.UpdateAsync(accountDTO);

//        assert
//        Assert.IsNotNull(initialAccount);
//        Assert.IsNotNull(updatedAccount);

//        Assert.IsTrue(initialAccount.AccountId == updatedAccount.AccountId);

//        this test only changes the password
//        Assert.AreNotSame(initialAccount.Password, updatedAccount.Password);
//    }

//    [Test]
//    public async Task Should_Update_and_Throw_ValdiationException()
//    {
//        arrange
//       var accountDTO = new BookingDTO()
//       {
//           BookingId = 3,
//           PhoneNumber = "1234567890",
//           AccountId = 3,
//           LocationId = 3,
//           InDate = "12-12-2022",
//           OutDate = "12-13-2022",
//           TotalPrice = 123

//       };
//        this.bookingsServiceMock.SetUpSearchByID(accountDTO.AccountId);
//        this.bookingsServiceMock.SetUpGetAll();
//        this.bookingsServiceMock.SetUpUpdate(accountDTO);
//        try
//        {
//            act
//           await bookingsService.UpdateAsync(accountDTO);

//            await bookingsService.GetAllAsync();

//            Assert.Fail();
//        }
//        catch (ValidationException ex)
//        {
//            assert

//            Assert.That(ex, Is.Not.Null);

//            Assert.That(ex.Message, Is.Not.Empty);
//        }

//    }
//}
