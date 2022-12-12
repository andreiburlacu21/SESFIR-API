using FluentValidation;
using NUnit.Framework;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service;
using SESIFIR.Services.Tests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESIFIR.Services.Tests;

public class LocationServiceTests
{
    private LocationsService LocationsService;
    private LocationsServiceMock LocationsServiceMock;

    [SetUp]
    public void Initialize()
    {
        this.LocationsServiceMock = new LocationsServiceMock();

        this.LocationsService = new LocationsService(LocationsServiceMock.Service, LocationsServiceMock.Validator, LocationsServiceMock.Mapper);
    }

    [Test]
    public async Task Should_SearchyById_and_Return_ExpectedValue()
    {
        // arrange
        var LocationDTO = new LocationDTO()
        {
            LocationId = 1,
            LocationName = "FC Chelsea Bucuresti SA",
            Address = "Bulevardul Tineretului 2, Bucuresti 040341",
            ImageLocation = "Test location 1 updated",
            PricePerHour = 100,
            LocationX = 44.40789806114942,
            LocationY = 26.1111227979398
        };
        this.LocationsServiceMock.SetUpSearchByID(LocationDTO.LocationId);

        // act
        var Location = await LocationsService.SearchByIdAsync(LocationDTO.LocationId);

        // assert
        Assert.That(Location, Is.Not.Null);

    }

    [Test]
    public async Task Should_SearchyById_and_Return_NotFound()
    {
        // arrange
        this.LocationsServiceMock.SetUpSearchByID(33);

        // act
        var Location = await LocationsService.SearchByIdAsync(33);

        // assert
        Assert.That(Location, Is.Null);
    }

    [Test]
    public async Task Should_Insert_and_Return_ExpectedValue()
    {
        // arrange
        var LocationDTO = new LocationDTO()
        {
            LocationId = 1,
            LocationName = "FC Chelsea Bucuresti SA",
            Address = "Bulevardul Tineretului 2, Bucuresti 040341",
            ImageLocation = "Test location 1 updated",
            PricePerHour = 100,
            LocationX = 44.40789806114942,
            LocationY = 26.1111227979398
        };
        this.LocationsServiceMock.SetUpSearchByID(LocationDTO.LocationId);
        this.LocationsServiceMock.SetUpGetAll();
        this.LocationsServiceMock.SetUpInsert(LocationDTO);

        // act
        var Location = await LocationsService.InsertAsync(LocationDTO);
        var Locations = await LocationsService.GetAllAsync();

        // assert
        Assert.IsNotNull(Location);
        Assert.IsTrue(Locations.Any(x => x.LocationId == Location.LocationId));
    }

    [Test]
    public async Task Should_Insert_and_Throw_ValdiationException()
    {
        // arrange
        var LocationDTO = new LocationDTO()
        {
            LocationId = 1,
            LocationName = "",
            Address = "Bulevardul Tineretului 2, Bucuresti 040341",
            ImageLocation = "Test location 1 updated",
            PricePerHour = 100,
            LocationX = 44.40789806114942,
            LocationY = 26.1111227979398
        };
        this.LocationsServiceMock.SetUpSearchByID(LocationDTO.LocationId);
        this.LocationsServiceMock.SetUpGetAll();
        this.LocationsServiceMock.SetUpInsert(LocationDTO);
        try
        {
            // act
            await LocationsService.InsertAsync(LocationDTO);

            await LocationsService.GetAllAsync();

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
        var LocationDTO = new LocationDTO()
        {
            LocationId = 1,
            LocationName = "FC Chelsea Bucuresti SA1",
            Address = "Bulevardul Tineretului 2, Bucuresti 040341",
            ImageLocation = "Test location 1 updated",
            PricePerHour = 100,
            LocationX = 44.40789806114942,
            LocationY = 26.1111227979398
        };
        this.LocationsServiceMock.SetUpSearchByID(LocationDTO.LocationId);
        this.LocationsServiceMock.SetUpGetAll();
        this.LocationsServiceMock.SetUpUpdate(LocationDTO);
        this.LocationsServiceMock.SetUpFirstOrDefault(x => x.LocationName == LocationDTO.LocationName);


        // act
        var initialLocation = await LocationsService.SearchByIdAsync(LocationDTO.LocationId);

        var updatedLocation = await LocationsService.UpdateAsync(LocationDTO);

        // assert
        Assert.IsNotNull(initialLocation);
        Assert.IsNotNull(updatedLocation);

        Assert.IsTrue(initialLocation.LocationId == updatedLocation.LocationId);

        //this test only changes the password
        Assert.AreNotSame(initialLocation.LocationName, updatedLocation.LocationName);
    }

    [Test]
    public async Task Should_Update_and_Throw_ValdiationException()
    {
        // arrange
        var LocationDTO = new LocationDTO()
        {
            LocationId = 1,
            LocationName = "",
            Address = "Bulevardul Tineretului 2, Bucuresti 040341",
            ImageLocation = "Test location 1 updated",
            PricePerHour = 100,
            LocationX = 44.40789806114942,
            LocationY = 26.1111227979398
        };
        this.LocationsServiceMock.SetUpSearchByID(LocationDTO.LocationId);
        this.LocationsServiceMock.SetUpGetAll();
        this.LocationsServiceMock.SetUpUpdate(LocationDTO);
        try
        {
            // act
            await LocationsService.UpdateAsync(LocationDTO);

            await LocationsService.GetAllAsync();

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
