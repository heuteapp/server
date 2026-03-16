namespace HeuteApp.Api.Models.Requests.Auth;

public record LoginRequest(
    string Identifier,
    string Password
);