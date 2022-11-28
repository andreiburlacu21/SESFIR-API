using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;
using SESFIR.Utils.Enums;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SESFIR.Controllers
{
    [Route("SESFIR/[controller]")]
    [ApiController]
    [Authorize]

    public class BookingsController : ControllerBase
    {
        private readonly IServiceBookings _bookingService;
        public BookingsController(IServiceBookings bookingService)
        {
            _bookingService = bookingService;
        }

        #region Crud Operation

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _bookingService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] BookingDTO booking)
        {
            try
            {
                return Ok(await _bookingService.InsertAsync(booking));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] BookingDTO booking)
        {
            try
            {
                await Check(booking.BookingId);

                return Ok(await _bookingService.UpdateAsync(booking));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Check(id);

                return Ok(await _bookingService.DeleteAsync(new BookingDTO { BookingId = id }));
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
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var bookingData = await _bookingService.SearchByIdAsync(id);

            if (bookingData.AccountId != userId || userRole != Role.Admin.ToString())
            {
                throw new ValidationException("You dont have access to modify thie value");
            }
        }
        #endregion
    }
}
