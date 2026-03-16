namespace HeuteApp.Api.Models.Requests.Auth;

public record SignInRequest(
    string Identifier,
    string Password
);