﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service;
using SESFIR.Services.Model.Service.Contracts;
using SESFIR.Utils.Enums;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SESFIR.Controllers
{
    [Route("SESFIR/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceReviews _reviewService;
        public ReviewsController(IServiceReviews reviewService)
        {
            _reviewService = reviewService;
        }

        #region Crud Operation

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _reviewService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Insert([FromBody] ReviewDTO review)
        {
            try
            {
                return Ok(await _reviewService.InsertAsync(review));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Update([FromBody] ReviewDTO review)
        {
            try
            {
                await Check(review.ReviewId);

                return Ok(await _reviewService.UpdateAsync(review));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Check(id);

                return Ok(await _reviewService.DeleteAsync(new ReviewDTO { ReviewId = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation

        [HttpGet("Entity/{id}")]
        public async Task<IActionResult> GetReviewEntityById(int id)
        {
            try
            {
                return Ok(await _reviewService.GetReviewEnitityAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("MyEntities")]
        public async Task<IActionResult> GetMyReviewEntities()
        {
            try
            {
                var id = int.Parse(User.FindFirst("Identifier")?.Value);

                return Ok(await _reviewService.GetMyReviewsEntitiesAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Private methods
        private async Task Check(int id)
        {
            var reviewId = int.Parse(User.FindFirst("Identifier")?.Value);

            var reviewData = await _reviewService.SearchByIdAsync(id);

            if (reviewData.AccountId != reviewId)
            {
                throw new ValidationException("You dont have access to modify thie value");
            }
        }
        #endregion
    }

}
