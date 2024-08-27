using Application.Services;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin model)
    {
        var token = _authService.Authenticate(model.Username, model.Password);

        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token });
    }
}

public class UserLogin
{
    public string Username { get; set; }
    public string Password { get; set; }
}
