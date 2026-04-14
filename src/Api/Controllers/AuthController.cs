using Microsoft.AspNetCore.Mvc;
using HeuteApp.Core.ValueObjects.Profile;
using HeuteApp.Api.Services.Singletons;
using HeuteApp.Api.Models.Requests.Auth;
using HeuteApp.Application.Services.Public;
using HeuteApp.Application.Services.Internal;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(
    InternalProfileService internalProfileService,
    PublicProfileService publicProfileService, 
    SupabaseProvider supabaseProvider,
    IConfiguration configuration) : ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest request)
    {
        var profile = await publicProfileService.GetProfileByIdentifierAsync(request.Identifier);

        if (profile == null)
            return NotFound();

        var session = await supabaseProvider.Client.Auth.SignIn(
            profile.Email,
            request.Password
        );

        if (session?.User == null)
            return Unauthorized();

        SetRefreshTokenCookie(session.RefreshToken!);

        return Ok(new
        {
            profile,
            accessToken = session.AccessToken,
            expiresIn = session.ExpiresIn
        });
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var session = await supabaseProvider.Client.Auth.SignUp(
            request.Email,
            request.Password,
            new()
            {
                RedirectTo = configuration["RedirectTo:Verification"]
            }
        );

        if (session?.User == null)
            return BadRequest();

        var profile = await publicProfileService.CreateProfileAsync(
            new ProfileDefinition(
                Guid.Parse(session.User.Id!),
                request.Username,
                request.Email
            )
        );

        return Ok(new
        {
            profile,
            message = "User created successfully"
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized(new { message = "Refresh token required" });

        try
        {
            var session = await supabaseProvider.Client.Auth.SetSession(
                accessToken: "",
                refreshToken: refreshToken,
                forceAccessTokenRefresh: true
            );
            
            if (session?.User == null)
            {
                Response.Cookies.Delete("refreshToken");
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            if (!string.IsNullOrEmpty(session.RefreshToken) && session.RefreshToken != refreshToken)
            {
                SetRefreshTokenCookie(session.RefreshToken);
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
        catch (Exception ex)
        {
            Response.Cookies.Delete("refreshToken");
            return Unauthorized(new { message = ex.Message });
        }
    }

    private void SetRefreshTokenCookie(string refreshToken)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            Path = "/"
        });
    }
}