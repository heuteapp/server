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