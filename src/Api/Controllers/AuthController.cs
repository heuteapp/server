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
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {        
        var profile = await publicProfileService.GetProfileByIdentifierAsync(request.Identifier);
        if (profile == null)
            return NotFound(new { message = "User not found" });

        try
        {
            var session = await supabaseProvider.Client.Auth.SignIn(profile.Email, request.Password);
            if (session?.User == null)
                return Unauthorized(new { message = "Invalid credentials" });

            SetRefreshTokenCookie(session.RefreshToken!);

            return Ok(new
            {
                profile,
                accessToken = session.AccessToken,
                expiresIn = session.ExpiresIn
            });
        }
        catch (Exception)
        {
            return Unauthorized(new { message = "Authentication failed" });
        }
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
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
                return BadRequest(new { message = "Sign up failed" });

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
                profile,
                message = "User created successfully. Please verify your email."
            });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Registration failed" });
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
            var session = await supabaseProvider.Client.Auth.RefreshSession();
            
            if (session?.User == null)
                return Unauthorized();

            SetRefreshTokenCookie(session.RefreshToken!);

            var profile = await internalProfileService.GetProfileByIdAsync(
                Guid.Parse(session.User.Id!)
            );

            return Ok(new
            {
                profile,
                accessToken = session.AccessToken
            });
        }
        catch (Exception ex)
        {
            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/" });
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