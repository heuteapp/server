namespace HeuteApp.Api.Models.Requests.Auth;

public record LoginRequest(
    string Name,
    string Password
);