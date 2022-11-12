﻿using Microsoft.AspNetCore.Mvc;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SESFIR.Controllers
{
    [Route("SESFIR/[controller]")]
    [ApiController]

    public class AccountsController : ControllerBase
    {
        private readonly IServiceAccounts _userService;
        public AccountsController(IServiceAccounts userService)
        {
            _userService = userService;
        }

        #region Crud Operation

        [HttpGet("all")]
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
        public async Task<IActionResult> Insert([FromBody] AccountsDTO user)
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
        public async Task<IActionResult> Update([FromBody] AccountsDTO user)
        {
            try
            {

                return Ok(await _userService.UpdateAsync(user));
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

                return Ok(await _userService.DeleteAsync(new AccountsDTO { AccountId = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation
        [HttpGet("username/{userName}")]
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
        //private async Task CheckRole(AccountsDTO user)
        //{
        //    var userId = int.Parse(User.FindFirst("Identifier")?.Value);
        //    var userData = await _userService.SearchByIdAsync(user.Id);
        //    var role = User.FindFirst(ClaimTypes.Role)?.Value;

        //    if (userId.Equals(user.Id) && !userData.IsAdmin.Equals(user.IsAdmin))
        //        throw new Exception("You can't edit your role, contact the owner for this task");

        //    if (!(role == "Admin" || user.Id == userId))
        //        throw new Exception("You don't have access to modify, view or insert this value");

        //}
        #endregion
    }
}