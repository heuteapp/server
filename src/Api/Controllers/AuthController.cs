using System.Text;
using HeuteApp.Application.Services;
using HeuteApp.Core.ValueObjects.Profile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HeuteApp.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(ProfileService profileService, HttpClient httpClient, IConfiguration configuration) : ControllerBase
{

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        var payload = new { email = request.Email, password = request.Password };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        content.Headers.Add("apikey", configuration["Supabase:AnonKey"]);

        var response = await httpClient.PostAsync(configuration["Supabase:Url"] + "/auth/v1/signup", content);
        if (!response.IsSuccessStatusCode)
            return BadRequest("Signup failed at Supabase.");

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<SupabaseSignupResponse>(json);

        if (result == null || result.User == null || result.Session == null)
            return BadRequest("Supabase signup failed or returned invalid data.");

        string userId = result.User.Id;

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
            result.Session.AccessToken,
            result.Session.RefreshToken
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // 1️⃣ Supabase login
        var payload = new { email = request.Name, password = request.Password };
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        content.Headers.Add("apikey", configuration["Supabase:AnonKey"]);

        var response = await httpClient.PostAsync(configuration["Supabase:Url"] + "/auth/v1/token?grant_type=password", content);
        if (!response.IsSuccessStatusCode)
            return Unauthorized("Login failed at Supabase.");

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<SupabaseLoginResponse>(json);

        if (result == null || result.User == null || result.Session == null)
            return Unauthorized("Supabase login failed or returned invalid data.");

        var profile = await profileService.GetProfileByKeyAsync(new ProfileKey(request.Name));
        if (profile == null)
            return NotFound("Profile not found for this user.");

        return Ok(new
        {
            profile,
            result.Session.AccessToken,
            result.Session.RefreshToken
        });
    }
}

public record SignupRequest(
    [property: JsonProperty("email")] string Email,
    [property: JsonProperty("password")] string Password,
    [property: JsonProperty("name")] string Name
);

public record SupabaseSignupResponse(
    [property: JsonProperty("user")] SupabaseUser User,
    [property: JsonProperty("session")] SupabaseSession Session
);

public record SupabaseUser(
    [property: JsonProperty("id")] string Id,
    [property: JsonProperty("email")] string Email
);

public record SupabaseSession(
    [property: JsonProperty("access_token")] string AccessToken,
    [property: JsonProperty("refresh_token")] string RefreshToken
);

public record LoginRequest(
    [property: JsonProperty("name")] string Name,
    [property: JsonProperty("password")] string Password
);

public record SupabaseLoginResponse(
    [property: JsonProperty("user")] SupabaseUser User,
    [property: JsonProperty("session")] SupabaseSession Session
);