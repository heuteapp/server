using HeuteApp.Api.Models.Public.Request;
using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetUser(string name)
    {
        var user = await userService.GetUserByKeyAsync(new (name));

        if(user == null)
            return NotFound("User not found for the given name.");

        return Ok(user);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateUser([FromBody] string name)
    {
        var user = await userService.CreateUserAsync(new (name), new ());

        return Ok(user);
    }
}