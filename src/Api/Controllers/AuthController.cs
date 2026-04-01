using HeuteApp.Core.ValueObjects.Profile;
using Microsoft.AspNetCore.Mvc;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Api.Models.Requests.Auth;
using HeuteApp.Application.Services.Public;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(
    PublicProfileService publicProfileService, 
    SupabaseProvider supabaseProvider) : ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {        
        var profile = await publicProfileService.GetProfileByIdentifierAsync(request.Identifier);
        if (profile == null)
            return NotFound();

        var session = await supabaseProvider.Client.Auth.SignIn(profile.Email, request.Password);
        if (session?.User == null)
            return Unauthorized();

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

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var session = await supabaseProvider.Client.Auth.SignUp(
            request.Email,
            request.Password, 
            new()
            {
                RedirectTo = "https://www.heuteapp.net/workspace/verification"
            }
        );

        if (session?.User == null)
            return BadRequest();

        string userId = session.User.Id!;

        var profile = await publicProfileService.CreateProfileAsync(
            new ProfileDefinition(
                Guid.Parse(userId),
                request.Username,
                request.Email
            )
        );

        return Ok(new
        {
            profile
        });
    }
}