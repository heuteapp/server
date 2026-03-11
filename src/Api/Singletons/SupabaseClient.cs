namespace HeuteApp.Api.Singletons;

public sealed class SupabaseClient(IConfiguration configuration)
{
    public Supabase.Client Client { get; } = new Supabase.Client(
        configuration["Supabase:Url"]!,
        configuration["Supabase:ServiceKey"]!
    );
}