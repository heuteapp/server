using HeuteApp.Application.Services;
using HeuteApp.Core.ValueObjects.Profile;
using Microsoft.AspNetCore.Mvc;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Api.Models.Requests.Auth;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(
    ProfileService profileService, SupabaseProvider supabaseProvider) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignUpRequest request)
    {
        var session = await supabaseProvider.Client.Auth.SignUp(request.Email, request.Password, new() { });
        if (session?.User == null)
            return BadRequest("Supabase signup failed or returned invalid data.");

        string userId = session.User.Id!;

        var profile = await profileService.CreateProfileAsync(
            new ProfileDefinition(
                Guid.Parse(userId),
                new ProfileProps(
                    request.Username,
                    request.Email
                )
            )
        );

        return Ok(new
        {
            profile
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {        
        var profile = await profileService.GetProfileByIdentifierAsync(request.Identifier);
        if (profile == null)
            return NotFound("Profile not found for this user.");

        var session = await supabaseProvider.Client.Auth.SignIn(profile.Email, request.Password);
        if (session?.User == null)
            return Unauthorized("Supabase login failed or returned invalid data.");

        Response.Cookies.Append("refreshToken", session.RefreshToken!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30)
        });

        return Ok(new
        {
            profile,
            session.AccessToken
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var accessToken = Request.Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last();

        if (string.IsNullOrEmpty(refreshToken)) return BadRequest();
        var session = await supabaseProvider.Client.Auth.SetSession(accessToken!, refreshToken, true);

        if (session?.AccessToken == null) return Unauthorized();

        return Ok(new
        {
            accessToken = session.AccessToken,
            profile = await profileService.GetProfileByIdAsync(Guid.Parse(session.User!.Id!))
        });
    }
}