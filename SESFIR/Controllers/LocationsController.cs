﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SESFIR.Controllers
{
    [Route("SESFIR/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IServiceLocations _locationService;
        public LocationsController(IServiceLocations locationService)
        {
            _locationService = locationService;
        }

        #region Crud Operation

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _locationService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Insert([FromBody] LocationDTO location)
        {
            try
            {
                return Ok(await _locationService.InsertAsync(location));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromBody] LocationDTO location)
        {
            try
            {

                return Ok(await _locationService.UpdateAsync(location));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                return Ok(await _locationService.DeleteAsync(new LocationDTO { LocationId = id }));
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
        //private async Task CheckRole(LocationsDTO location)
        //{
        //    var locationId = int.Parse(User.FindFirst("Identifier")?.Value);
        //    var locationData = await _locationService.SearchByIdAsync(location.Id);
        //    var role = User.FindFirst(ClaimTypes.Role)?.Value;

        //    if (locationId.Equals(location.Id) && !locationData.IsAdmin.Equals(location.IsAdmin))
        //        throw new Exception("You can't edit your role, contact the owner for this task");

        //    if (!(role == "Admin" || location.Id == locationId))
        //        throw new Exception("You don't have access to modify, view or insert this value");

        //}
        #endregion
    }

}
