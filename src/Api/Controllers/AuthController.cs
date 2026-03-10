using HeuteApp.Application.Services;
using HeuteApp.Core.ValueObjects.Profile;
using Microsoft.AspNetCore.Mvc;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(ProfileService profileService, IConfiguration configuration) : ControllerBase
{
    private readonly Supabase.Client supabaseClient = new(
        configuration["Supabase:Url"]!,
        configuration["Supabase:ServiceKey"]!
    );

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        // 1️⃣ Supabase signup
        var session = await supabaseClient.Auth.SignUp(request.Email, request.Password, new() { });
        if (session?.User == null)
            return BadRequest("Supabase signup failed or returned invalid data.");

        string userId = session.User.Id!;

        // 2️⃣ Profile oluştur
        var profile = await profileService.CreateProfileAsync(
            new ProfileOwnership(Guid.Parse(userId)),
            new ProfileDefinition(
                new ProfileKey(request.Name),
                new ProfileProps()
            )
        );

        // 3️⃣ Response
        return Ok(new
        {
            profile,
            session.AccessToken,
            session.RefreshToken
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // 1️⃣ Supabase login
        var session = await supabaseClient.Auth.SignIn(request.Name, request.Password);
        if (session?.User == null)
            return Unauthorized("Supabase login failed or returned invalid data.");

        // 2️⃣ Profile getir
        var profile = await profileService.GetProfileByKeyAsync(new ProfileKey(request.Name));
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

public record SignupRequest(string Email, string Password, string Name);

public record LoginRequest(string Name, string Password);