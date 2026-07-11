using Microsoft.AspNetCore.Mvc;
using Module6.WebApi.Models.Auth;
using Module6.WebApi.Services;

namespace Module6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserStore _userStore;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IUserStore userStore, IJwtTokenService jwtTokenService)
    {
        _userStore = userStore;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userStore.Validate(request.Username, request.Password);
        if (user is null)
            return Unauthorized(new { message = "Invalid username or password." });

        var (token, expiresAtUtc) = _jwtTokenService.GenerateToken(user.Username, user.Role);

        return Ok(new LoginResponse
        {
            Token = token,
            ExpiresAtUtc = expiresAtUtc,
            Role = user.Role
        });
    }
}
