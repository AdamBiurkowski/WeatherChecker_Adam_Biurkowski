using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WeatherChecker_Adam_Biurkowski.Intrefaces;
using WeatherChecker_Adam_Biurkowski.Models;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IJwtTokenService _jwtService;

    public AccountController(IAccountService accountService, IJwtTokenService jwtService)
    {
        _accountService = accountService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Account account)
    {
        var success = await _accountService.RegisterAsync(account.Email, account.PasswordHash);
        if (!success)
            return BadRequest("User already exists");

        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Account account)
    {
        var isValid = await _accountService.VerifyPasswordAsync(account.Email, account.PasswordHash);
        if (!isValid)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateToken(account.Email);
        return Ok(token);
    }
}

