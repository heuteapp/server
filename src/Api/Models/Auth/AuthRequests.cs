namespace HeuteApp.Api.Models.Auth;

public record SignupRequest(
    string Name, 
    string Email, 
    string Password
);

public record LoginRequest(
    string Name, 
    string Password
);