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
                        .WithOrigins("http://localhost:3000", "https://heuteapp.net", "https://www.heuteapp.net")
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