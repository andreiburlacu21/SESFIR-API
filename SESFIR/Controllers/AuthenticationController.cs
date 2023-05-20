using Microsoft.AspNetCore.Mvc;
using SESFIR.DTOs;
using SESFIR.Services.Authentication.Service.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SESFIR.Controllers;

[Route("SESFIR/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    // private readonly IEmailService _emailService;
    //public AuthenticationController(IAuthenticationService authService, IEmailService emailService = null)
    //{
    //    _authService = authService;
    //    _emailService = emailService;
    //}
    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;    
    }

    // POST api/<AuthController>
    [HttpPost]
    public async Task<IActionResult> GetToken(AuthDTO authData)
    {
        try
        {
            return Ok(await _authService.GenerateTokenAsync(authData));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountDTO user)
    {
        try
        {
            var result = await _authService.RegisterAsync(user);

            //await _emailService.SendCreatedEmailAsync(result.Email);

            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("checkLogin")]
    public IActionResult Check()
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);

            if (userId > 0) return Ok(true);
        }
        catch { }

        return Ok(false);

    }

    [HttpGet("check")]
    public IActionResult Check1()
    {

        return Ok("da");

    }
}
