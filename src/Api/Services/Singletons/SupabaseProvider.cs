namespace HeuteApp.Api.Services.Singletons;

public sealed class SupabaseProvider
{
    public Supabase.Client Client { get; }

    public SupabaseProvider(IConfiguration configuration)
    {
        string url = Environment.GetEnvironmentVariable("SUPABASE_URL") 
        ?? configuration["Supabase:Url"]!;

        string key = Environment.GetEnvironmentVariable("SUPABASE_KEY") 
        ?? configuration["Supabase:Key"]!;

        Client = new Supabase.Client(url, key);
    }
}
