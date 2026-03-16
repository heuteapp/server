using HeuteApp.Api.Services.Contexts;
using HeuteApp.Api.Services.Singletons;

namespace HeuteApp.Api.Services.Middlewares;

public class AuthMiddleware(RequestDelegate next, SupabaseProvider supabaseProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userContext = context.RequestServices.GetRequiredService<UserContext>();

        var accessToken = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        var refreshToken = context.Request.Cookies["refreshToken"];

        if(!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
        {
            await supabaseProvider.Client.Auth.SetSession(accessToken, refreshToken, true);
            var user = supabaseProvider.Client.Auth.CurrentUser;
            if(user != null && user.Id != null)
            {
                userContext.SetUser(Guid.Parse(user.Id));
            }
        }

        await next(context);
    }
}