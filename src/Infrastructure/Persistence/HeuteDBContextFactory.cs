using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContextFactory : IDesignTimeDbContextFactory<HeuteDbContext>
{
    public HeuteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HeuteDbContext>();

        optionsBuilder.UseNpgsql(
                "Host=aws-1-ap-south-1.pooler.supabase.com;" +
    "Port=5432;" +
    "Database=postgres;" +
    "Username=postgres.zzgoxhdrnwbqlklwtxrd;" +
    "Password=dwL9GI14mlNbNVBQ;" +
    "Ssl Mode=Require;" +
    "Trust Server Certificate=true"
        );
            
        return new HeuteDbContext(optionsBuilder.Options);
    }
}