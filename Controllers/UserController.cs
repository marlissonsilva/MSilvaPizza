using MSilvaPizza.Models;
using MSilvaPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace MSilvaPizza.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]


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
    public async Task<IActionResult> Create(User user)
    {
        var newUser = await _userService.Create(user);
        return CreatedAtAction(nameof(Get), new { uuid = newUser.Uuid }, newUser);
    }

    [HttpPatch("{uuid}")]
    public async Task<IActionResult> Update(Guid uuid, User user)
    {
        if (uuid != user.Uuid)
            return BadRequest();

        var updated = await _userService.Update(uuid, user);
        if (!updated) return NotFound();
        return NoContent();
    }
}