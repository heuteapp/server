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
                    RedirectTo = configuration["AppSettings:VerificationRedirectUrl"]
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