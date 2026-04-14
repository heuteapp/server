using HeuteApp.Api.Services.Singletons;
using HeuteApp.Application.Interfaces.UserBased;

namespace HeuteApp.Api.Services.Middlewares;

public class AuthMiddleware(RequestDelegate next, SupabaseProvider supabaseProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userContext = context.RequestServices.GetRequiredService<IUserContext>();
        var accessToken = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(accessToken))
        {
            await next(context);
            return;
        }

        var isValid = await ValidateOrRefreshToken(context, accessToken, userContext);
        
        if (!isValid)
        {
            context.Response.StatusCode = 401;
            return;
        }

        await next(context);
    }
    
    private async Task<bool> ValidateOrRefreshToken(HttpContext context, string accessToken, IUserContext userContext)
    {
        try
        {
            var user = await supabaseProvider.Client.Auth.GetUser(accessToken);
            
            if (user?.Id != null)
            {
                userContext.SetUser(Guid.Parse(user.Id));
                return true;
            }
            
            return false;
        }
        catch
        {
            return await TryRefreshToken(context, userContext);
        }
    }
    
    private async Task<bool> TryRefreshToken(HttpContext context, IUserContext userContext)
    {
        var refreshToken = context.Request.Cookies["refreshToken"];
        
        if (string.IsNullOrEmpty(refreshToken))
            return false;
        
        try
        {
            var session = await supabaseProvider.Client.Auth.SetSession(
                accessToken: "", 
                refreshToken: refreshToken,
                forceAccessTokenRefresh: true
            );
            
            if (session?.User == null)
                return false;
            
            userContext.SetUser(Guid.Parse(session.User.Id!));
            
            context.Response.Headers.Append("X-New-Access-Token", session.AccessToken);
            
            if (!string.IsNullOrEmpty(session.RefreshToken) && session.RefreshToken != refreshToken)
            {
                UpdateRefreshTokenCookie(context, session.RefreshToken);
            }
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Refresh token failed: {ex.Message}");
            ClearRefreshTokenCookie(context);
            return false;
        }
    }
    
    private static void UpdateRefreshTokenCookie(HttpContext context, string newRefreshToken)
    {
        context.Response.Cookies.Delete("refreshToken");
        context.Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            Path = "/"
        });
    }
    
    private static void ClearRefreshTokenCookie(HttpContext context)
    {
        context.Response.Cookies.Delete("refreshToken");
    }
}