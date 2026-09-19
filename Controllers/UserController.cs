using MSilvaPizza.Models;
using MSilvaPizza.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace MSilvaPizza.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    public UserController(IUserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    [HttpGet("{uuid}")]
    public async Task<ActionResult<User>> Get(Guid uuid)
    {
        var user = await _userService.GetByUuid(uuid);
        if (user is null)
        {
            return NotFound();
        }
        return user;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterRequest request)
    {
        var exists = await _userService.UsernameExists(request.Username);
        if (exists)
        {
            return Conflict(new { message = "Este nome de usuário já está em uso. Por favor, escolha outro." });
        }
        var username = new User { Username = request.Username };
        var newUser = await _userService.Create(username, request.Password);
        return CreatedAtAction(nameof(Get), new { uuid = newUser.Uuid }, newUser);
    }

    [HttpPatch("{uuid}")]
    public async Task<IActionResult> Update(Guid uuid, User user)
    {
        if (uuid != user.Uuid)
            return BadRequest();

        var updated = await _userService.Update(uuid, user, user.Password);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("jwt_token");
        return Ok(new { message = "Usuário deslogado com sucesso" });
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var jwtToken = Request.Cookies["jwt_token"];
        if (string.IsNullOrEmpty(jwtToken))
        {
            return Unauthorized(new { message = "Token JWT não encontrado" });
        }
        return Ok(new { message = "Sesseão ativa" });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.GetByUsername(request.Username);
        if (user is null)
            return Unauthorized(new { message = "Usuário ou senha incorretos" });

        var isValidPassword = await _userService.Login(user, user.Password, request.Password);

        if (!isValidPassword)
            return Unauthorized(new { message = "Usuário ou senha incorretos" });

        var token = GenerateJwtToken(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(1)
        };

        Response.Cookies.Append("jwt_token", token, cookieOptions);

        return Ok(new { message = "Usuário autenticado com sucesso" });
    }

    private string GenerateJwtToken(User user)
    {
        var keyString = _config["Jwt:Secret"];
        var key = Encoding.ASCII.GetBytes(keyString);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Uuid.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}