namespace HeuteApp.Api.Models.Public.Request;

public record SignUpRequest(
    string Name, 
    string Email, 
    string Password
);

public record SignInRequest(
    string Name, 
    string Password
);