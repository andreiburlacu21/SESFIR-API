using FluentValidation;
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
        public async Task<IActionResult> Insert([FromBody] ReviewsDTO review)
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

        public async Task<IActionResult> Update([FromBody] ReviewsDTO review)
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

                return Ok(await _reviewService.DeleteAsync(new ReviewsDTO { ReviewId = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation

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
