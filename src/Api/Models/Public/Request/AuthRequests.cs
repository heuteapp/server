namespace HeuteApp.Api.Models.Public.Request;

public record SignupRequest(
    string Name, 
    string Email, 
    string Password
);

public record LoginRequest(
    string Name, 
    string Password
);