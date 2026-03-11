namespace HeuteApp.Api.Services.Singletons;

public sealed class SupabaseProvider(IConfiguration configuration)
{
    public Supabase.Client Client { get; } = new Supabase.Client(
        configuration["Supabase:Url"]!,
        configuration["Supabase:ServiceKey"]!
    );
}