using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using HeuteApp.Api.Services.Contexts;

namespace HeuteApp.Api.Services.Middlewares;

public sealed class JwtMiddleware(RequestDelegate next, IConfiguration config)
{
    public async Task InvokeAsync(HttpContext context, UserContext userContext)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(config["Jwt:Secret"]!);
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
                {
                    userContext.SetUser(userId);
                }
            }
            catch {}
        }

        await next(context);
    }
}