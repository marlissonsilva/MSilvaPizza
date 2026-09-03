using MSilvaPizza.Models;
using MSilvaPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace MSilvaPizza.Controllers;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.GetByUsername(request.Username);
        if (user is null)
            return Unauthorized(new { message = "Usuário ou senha incorretos" });

        var isValidPassword = await _userService.Login(user, user.Password, request.Password);

        if (!isValidPassword)
            return Unauthorized(new { message = "Usuário ou senha incorretos" });

        return Ok(new { message = "Usuário autenticado com sucesso" });
    }
}