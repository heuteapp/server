using HeuteApp.Application.Services;
using HeuteApp.Core.ValueObjects.Profile;
using Microsoft.AspNetCore.Mvc;
using HeuteApp.Api.Models.Public.Request;
using HeuteApp.Api.Services.Singletons;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(
    ProfileService profileService, SupabaseProvider supabaseProvider) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var session = await supabaseProvider.Client.Auth.SignUp(request.Email, request.Password, new() { });
        if (session?.User == null)
            return BadRequest("Supabase signup failed or returned invalid data.");

        string userId = session.User.Id!;

        var profile = await profileService.CreateProfileAsync(
            new ProfileDefinition(
                Guid.Parse(userId),
                new ProfileProps(
                    request.Name,
                    request.Email
                )
            )
        );

        // 3️⃣ Response
        return Ok(new
        {
            profile
        });
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var session = await supabaseProvider.Client.Auth.SignIn(request.Name, request.Password);
        if (session?.User == null)
            return Unauthorized("Supabase login failed or returned invalid data.");

        var profile = await profileService.GetProfileByNameAsync(request.Name);
        if (profile == null)
            return NotFound("Profile not found for this user.");

        return Ok(new
        {
            profile,
            session.AccessToken,
            session.RefreshToken
        });
    }
}