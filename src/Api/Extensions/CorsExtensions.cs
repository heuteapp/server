namespace HeuteApp.Api.Extensions;

public static class CorsExtensions
{
    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    public static void UseAppCors(this WebApplication app)
    {
        app.UseCors("AllowFrontend");
    }
}