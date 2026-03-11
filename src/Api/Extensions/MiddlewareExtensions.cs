using HeuteApp.Api.Services.Middlewares;

namespace HeuteApp.Api.Extensions;

public static class MiddlewareExtensions
{
    public static void UseAppMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<JwtMiddleware>();
    }
}