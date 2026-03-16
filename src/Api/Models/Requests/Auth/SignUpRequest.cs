namespace HeuteApp.Api.Models.Requests.Auth;

public record SignUpRequest(
    string Username,
    string Email,
    string Password
);