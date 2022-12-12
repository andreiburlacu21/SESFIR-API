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

internal class ReviewsServiceMock
{
    #region Fields
    private Mock<ISQLDataFactory> mockRepositories;
    private IMapper mockMapper;
    private IValidator<ReviewDTO> mockValidator;
    private List<Review> mockReviews;
    #endregion

    #region Contructors
    public ReviewsServiceMock()
    {
        this.mockRepositories = new Mock<ISQLDataFactory>();

        this.mockValidator = new ReviewsValidation();

        this.mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new SQLDatabaseProfile());

        }).CreateMapper();

        this.SetUpMockReviews();
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
    public IValidator<ReviewDTO> Validator
    {
        get
        {
            return mockValidator;
        }
    }
    #endregion

    #region Methods
    public void SetUpInsert(ReviewDTO accountDTO)
    {
        mockRepositories.Setup(x => x.ReviewsRepository.InsertAsync(It.IsAny<Review>()))
            .ReturnsAsync(() =>
            {
                var account = this.Mapper.Map<Review>(accountDTO);

                var count = mockReviews.Count + 1;

                account.ReviewId = count;

                mockReviews.Add(account);

                return mockReviews.FirstOrDefault(x => x.ReviewId == account.ReviewId);
            });
    }
    public void SetUpUpdate(ReviewDTO accountDTO)
    {
        mockRepositories.Setup(x => x.ReviewsRepository.UpdateAsync(It.IsAny<Review>()))
            .ReturnsAsync(() =>
            {
                var account = this.Mapper.Map<Review>(accountDTO);

                mockReviews.RemoveAll(x => x.ReviewId == account.ReviewId);

                mockReviews.Add(account);

                return mockReviews.FirstOrDefault(x => x.ReviewId == account.ReviewId);
            });
    }
    public void SetUpGetAll()
    {
        mockRepositories.Setup(x => x.ReviewsRepository.GetAllAsync())
            .ReturnsAsync(() =>
            {
                return mockReviews;
            });
    }
    public void SetUpFirstOrDefault(Func<Review, bool> expresion)
    {
        mockRepositories.Setup(x => x.ReviewsRepository.FirstOrDefaultAsync(It.IsAny<Func<Review, bool>>()))
           .ReturnsAsync(() =>
           {
               return mockReviews.FirstOrDefault(expresion);
           });
    }
    public void SetUpSearchByID(int id)
    {
        mockRepositories.Setup(x => x.ReviewsRepository.SearchByIdAsync(id))
            .ReturnsAsync(() =>
            {
                return mockReviews.FirstOrDefault(x => x.ReviewId == id);
            });
    }

    public void SetUpBookingForInsert()
    {
        mockRepositories.Setup(x => x.BookingsRepository.FirstOrDefaultAsync(It.IsAny<Func<Booking, bool>>()))
            .ReturnsAsync(() =>
            {
                return new Booking();
            });
    }
    private void SetUpMockReviews()
    {
        mockReviews = new List<Review>()
                {
                    new Review()
                        {
                            ReviewId = 1,
                            AccountId = 7,
                            LocationId = 1,
                            Grade = 5,
                            Description = "I will come again for sure!",
                            Date = "Thu Dec 08 2022"
                    },
                    new Review()
                        {
                            ReviewId = 2,
                            AccountId = 7,
                            LocationId = 1,
                            Grade = 5,
                            Description = "I will come again for sure!",
                            Date = "Thu Dec 08 2022"
                    },
                    new Review()
                        {
                            ReviewId = 3,
                            AccountId = 7,
                            LocationId = 1,
                            Grade = 5,
                            Description = "I will come again for sure!",
                            Date = "Thu Dec 08 2022"
                    }
};
    }
    #endregion
}
