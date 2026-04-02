using HeuteApp.Api.Services.Scopes;
using HeuteApp.Application.Interfaces.UserBased;

namespace HeuteApp.Api.Services.Middlewares;

public class AuthMiddleware(RequestDelegate next, SupabaseProvider supabaseProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userContext = context.RequestServices.GetRequiredService<IUserContext>();

        var accessToken = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(accessToken))
        {
            try
            {
                var user = await supabaseProvider.Client.Auth.GetUser(accessToken);
                
                if (user?.Id != null)
                {
                    userContext.SetUser(Guid.Parse(user.Id));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auth error: {ex.Message}");
            }
        }

        await next(context);
    }
}