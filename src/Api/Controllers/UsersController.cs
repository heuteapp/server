using HeuteApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("profiles")]
public class ProfilesController(ProfileService profileService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetProfile(string name)
    {
        var profile = await profileService.GetProfileByKeyAsync(new (name));

        if(profile == null)
            return NotFound("Profile not found for the given name.");

        return Ok(profile);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateProfile([FromBody] string name)
    {
        var profile = await profileService.CreateProfileAsync(new (name), new ());

        return Ok(profile);
    }
}