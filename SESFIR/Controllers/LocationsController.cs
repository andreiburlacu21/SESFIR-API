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
        public async Task<IActionResult> Insert([FromBody] LocationsDTO user)
        {
            try
            {
                return Ok(await _locationService.InsertAsync(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] LocationsDTO user)
        {
            try
            {

                return Ok(await _locationService.UpdateAsync(user));
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

                return Ok(await _locationService.DeleteAsync(new LocationsDTO { LocationId = id }));
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
        //private async Task CheckRole(LocationsDTO user)
        //{
        //    var userId = int.Parse(User.FindFirst("Identifier")?.Value);
        //    var userData = await _locationService.SearchByIdAsync(user.Id);
        //    var role = User.FindFirst(ClaimTypes.Role)?.Value;

        //    if (userId.Equals(user.Id) && !userData.IsAdmin.Equals(user.IsAdmin))
        //        throw new Exception("You can't edit your role, contact the owner for this task");

        //    if (!(role == "Admin" || user.Id == userId))
        //        throw new Exception("You don't have access to modify, view or insert this value");

        //}
        #endregion
    }

}
