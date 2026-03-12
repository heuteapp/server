namespace HeuteApp.Api.Models.Requests.Auth;

public record SignupRequest(
    string Name,
    string Email,
    string Password
);