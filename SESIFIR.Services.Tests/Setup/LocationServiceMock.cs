using AutoMapper;
using FluentValidation;
using Moq;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Mappers;
using SESFIR.Validators.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESIFIR.Services.Tests.Setup;

internal class LocationsServiceMock
{
    #region Fields
    private Mock<ISQLDataFactory> mockRepositories;
    private IMapper mockMapper;
    private IValidator<LocationDTO> mockValidator;
    private List<Location> mockLocations;
    #endregion

    #region Contructors
    public LocationsServiceMock()
    {
        this.mockRepositories = new Mock<ISQLDataFactory>();

        this.mockValidator = new LocationsValidation();

        this.mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new SQLDatabaseProfile());

        }).CreateMapper();

        this.SetUpMockLocations();
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
    public IValidator<LocationDTO> Validator
    {
        get
        {
            return mockValidator;
        }
    }
    #endregion

    #region Methods
    public void SetUpInsert(LocationDTO accountDTO)
    {
        mockRepositories.Setup(x => x.LocationsRepository.InsertAsync(It.IsAny<Location>()))
            .ReturnsAsync(() =>
            {
                var account = this.Mapper.Map<Location>(accountDTO);

                var count = mockLocations.Count + 1;

                account.LocationId = count;

                mockLocations.Add(account);

                return mockLocations.FirstOrDefault(x => x.LocationId == account.LocationId);
            });
    }
    public void SetUpUpdate(LocationDTO accountDTO)
    {
        mockRepositories.Setup(x => x.LocationsRepository.UpdateAsync(It.IsAny<Location>()))
            .ReturnsAsync(() =>
            {
                var account = this.Mapper.Map<Location>(accountDTO);

                mockLocations.RemoveAll(x => x.LocationId == account.LocationId);

                mockLocations.Add(account);

                return mockLocations.FirstOrDefault(x => x.LocationId == account.LocationId);
            });
    }
    public void SetUpGetAll()
    {
        mockRepositories.Setup(x => x.LocationsRepository.GetAllAsync())
            .ReturnsAsync(() =>
            {
                return mockLocations;
            });
    }
    public void SetUpFirstOrDefault(Func<Location, bool> expresion)
    {
        mockRepositories.Setup(x => x.LocationsRepository.FirstOrDefaultAsync(It.IsAny<Func<Location, bool>>()))
           .ReturnsAsync(() =>
           {
               return mockLocations.FirstOrDefault(expresion);
           });
    }
    public void SetUpSearchByID(int id)
    {
        mockRepositories.Setup(x => x.LocationsRepository.SearchByIdAsync(id))
            .ReturnsAsync(() =>
            {
                return mockLocations.FirstOrDefault(x => x.LocationId == id);
            });
    }
    private void SetUpMockLocations()
    {
        mockLocations = new List<Location>()
                    {
                        new Location()
                            {
                            LocationId= 1,
                            LocationName= "FC Chelsea Bucuresti SA",
                            Address= "Bulevardul Tineretului 2, Bucuresti 040341",
                            ImageLocation= "Test location 1 updated",
                            PricePerHour= 100,
                            LocationX= 44.40789806114942,
                            LocationY= 26.1111227979398
                        },
                        new Location()
                            {
                            LocationId= 1,
                            LocationName= "FC Chelsea Bucuresti SA",
                            Address= "Bulevardul Tineretului 2, Bucuresti 040341",
                            ImageLocation= "Test location 1 updated",
                            PricePerHour= 100,
                            LocationX= 44.40789806114942,
                            LocationY= 26.1111227979398
                        },
                        new Location()
                            {
                            LocationId= 1,
                            LocationName= "FC Chelsea Bucuresti SA",
                            Address= "Bulevardul Tineretului 2, Bucuresti 040341",
                            ImageLocation= "Test location 1 updated",
                            PricePerHour= 100,
                            LocationX= 44.40789806114942,
                            LocationY= 26.1111227979398
                        }
    };
    }
    #endregion
}