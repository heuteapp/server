using Microsoft.AspNetCore.Mvc;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Services.Internal;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("me")]
public class MeController(
    InternalProfileService internalProfileService,
    SupabaseProvider supabaseProvider) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Me()
    {
        var accessToken = Request.Headers.Authorization
            .FirstOrDefault()?
            .Split(" ")
            .Last();
        if (string.IsNullOrEmpty(accessToken))
            return Unauthorized(new { message = "Access token required" });

        try
        {
            var user = await supabaseProvider.Client.Auth.GetUser(accessToken);
            
            if (user == null)
                return Unauthorized();

            var profile = await internalProfileService.GetProfileByIdAsync(
                Guid.Parse(user.Id!)
            );

            if (profile == null)
                return NotFound(new { message = "Profile not found" });

            return Ok(profile);
        }
        catch (Exception)
        {
            return Unauthorized(new { message = "Invalid or expired token" });
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest(new { message = "Refresh token required" });

        try
        {
            var session = await supabaseProvider.Client.Auth.SetSession("", refreshToken);
            
            if (session?.User == null)
                return Unauthorized();

            if (!string.IsNullOrEmpty(session.RefreshToken))
            {
                Response.Cookies.Append("refreshToken", session.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    Path = "/"
                });
            }

            var profile = await internalProfileService.GetProfileByIdAsync(
                Guid.Parse(session.User.Id!)
            );

            return Ok(new
            {
                profile,
                accessToken = session.AccessToken,
                expiresIn = session.ExpiresIn
            });
        }
        catch
        {
            return Unauthorized();
        }
    }
}