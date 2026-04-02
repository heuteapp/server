using Microsoft.AspNetCore.Mvc;
using HeuteApp.Api.Services.Scopes;
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
                return Unauthorized(new { message = "Invalid token" });

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
}