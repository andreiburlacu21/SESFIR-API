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

public class ReviewServiceTests
{
    private ReviewsService ReviewsService;
    private ReviewsServiceMock ReviewsServiceMock;

    [SetUp]
    public void Initialize()
    {
        this.ReviewsServiceMock = new ReviewsServiceMock();

        this.ReviewsService = new ReviewsService(ReviewsServiceMock.Service, ReviewsServiceMock.Validator, ReviewsServiceMock.Mapper);
    }

    [Test]
    public async Task Should_SearchyById_and_Return_ExpectedValue()
    {
        // arrange
        var ReviewDTO = new ReviewDTO()
        {
            ReviewId = 1,
            AccountId = 7,
            LocationId = 1,
            Grade = 5,
            Description = "I will come again for sure!",
            Date = "Thu Dec 08 2022"
        };
        this.ReviewsServiceMock.SetUpSearchByID(ReviewDTO.ReviewId);

        // act
        var Review = await ReviewsService.SearchByIdAsync(ReviewDTO.ReviewId);

        // assert
        Assert.That(Review, Is.Not.Null);

    }

    [Test]
    public async Task Should_SearchyById_and_Return_NotFound()
    {
        // arrange
        this.ReviewsServiceMock.SetUpSearchByID(33);

        // act
        var Review = await ReviewsService.SearchByIdAsync(33);

        // assert
        Assert.That(Review, Is.Null);
    }

    [Test]
    public async Task Should_Insert_and_Return_ExpectedValue()
    {
        // arrange
        var ReviewDTO = new ReviewDTO()
        {
            ReviewId = 1,
            AccountId = 7,
            LocationId = 1,
            Grade = 5,
            Description = "I will come again for sure!",
            Date = "Thu Dec 08 2022"
        };
        this.ReviewsServiceMock.SetUpSearchByID(ReviewDTO.ReviewId);
        this.ReviewsServiceMock.SetUpGetAll();
        this.ReviewsServiceMock.SetUpInsert(ReviewDTO);
        this.ReviewsServiceMock.SetUpBookingForInsert();

        // act
        var Review = await ReviewsService.InsertAsync(ReviewDTO);
        var Reviews = await ReviewsService.GetAllAsync();

        // assert
        Assert.IsNotNull(Review);
        Assert.IsTrue(Reviews.Any(x => x.ReviewId == Review.ReviewId));
    }

    [Test]
    public async Task Should_Insert_and_Throw_ValdiationException()
    {
        // arrange
        var ReviewDTO = new ReviewDTO()
        {
            ReviewId = 1,
            AccountId = 7,
            LocationId = 1,
            Grade = 5,
            Description = "",
            Date = "Thu Dec 08 2022"
        };
        this.ReviewsServiceMock.SetUpSearchByID(ReviewDTO.ReviewId);
        this.ReviewsServiceMock.SetUpGetAll();
        this.ReviewsServiceMock.SetUpInsert(ReviewDTO);
        try
        {
            // act
            await ReviewsService.InsertAsync(ReviewDTO);

            await ReviewsService.GetAllAsync();

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
        var ReviewDTO = new ReviewDTO()
        {
            ReviewId = 1,
            AccountId = 7,
            LocationId = 1,
            Grade = 5,
            Description = "I will come again for sure1!",
            Date = "Thu Dec 08 2022"
        };
        this.ReviewsServiceMock.SetUpSearchByID(ReviewDTO.ReviewId);
        this.ReviewsServiceMock.SetUpGetAll();
        this.ReviewsServiceMock.SetUpUpdate(ReviewDTO);
        this.ReviewsServiceMock.SetUpFirstOrDefault(x => x.Description == ReviewDTO.Description);


        // act
        var initialReview = await ReviewsService.SearchByIdAsync(ReviewDTO.ReviewId);

        var updatedReview = await ReviewsService.UpdateAsync(ReviewDTO);

        // assert
        Assert.IsNotNull(initialReview);
        Assert.IsNotNull(updatedReview);

        Assert.IsTrue(initialReview.ReviewId == updatedReview.ReviewId);

        //this test only changes the Description
        Assert.AreNotSame(initialReview.Description, updatedReview.Description);
    }

    [Test]
    public async Task Should_Update_and_Throw_ValdiationException()
    {
        // arrange
        var ReviewDTO = new ReviewDTO()
        {
            ReviewId = 1,
            AccountId = 7,
            LocationId = 1,
            Grade = 5,
            Description = "",
            Date = "Thu Dec 08 2022"
        };
        this.ReviewsServiceMock.SetUpSearchByID(ReviewDTO.ReviewId);
        this.ReviewsServiceMock.SetUpGetAll();
        this.ReviewsServiceMock.SetUpUpdate(ReviewDTO);
        try
        {
            // act
            await ReviewsService.UpdateAsync(ReviewDTO);

            await ReviewsService.GetAllAsync();

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
