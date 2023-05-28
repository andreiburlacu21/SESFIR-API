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
    public class AccountsController : ControllerBase
    {
        private readonly IServiceAccounts _userService;
        public AccountsController(IServiceAccounts userService)
        {
            _userService = userService;
        }

        #region Crud Operation

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Insert([FromBody] AccountDTO user)
        {
            try
            {
                return Ok(await _userService.InsertAsync(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Update([FromBody] AccountDTO user)
        {
            try
            {
                await CheckRole(user);

                return Ok(await _userService.UpdateAsync(user));
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

                return Ok(await _userService.DeleteAsync(new AccountDTO { AccountId = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation
        [HttpGet("username/{userName}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> SearchByUserName(string userName)
        {
            try
            {
                var person = await _userService.SearchByUserNameAsync(userName);
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("myProfile")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> GetMyData()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                var person = await _userService.SearchByIdAsync(userId);
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> SearchByEmail(string email)
        {
            try
            {
                var person = await _userService.SearchByEmailAsync(email);
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Private methods
        private async Task CheckRole(AccountDTO user)
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            var userData = await _userService.SearchByIdAsync(user.AccountId);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId.Equals(user.AccountId) && userData.Role != user.Role)
                throw new ValidationException("You can't edit your role, contact the owner for this task");

            if (user.AccountId != userId && role != Role.Admin.ToString())
                throw new ValidationException("You don't have access to modify, view or insert this value");

        }
        #endregion
    }
}
