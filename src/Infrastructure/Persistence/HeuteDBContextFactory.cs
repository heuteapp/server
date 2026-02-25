using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContextFactory : IDesignTimeDbContextFactory<HeuteDbContext>
{
    public HeuteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HeuteDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=db.zzgoxhdrnwbqlklwtxrd.supabase.co;Database=postgres;Username=postgres;Password=dwL9GI14mlNbNVBQ;SSL Mode=Require;Trust Server Certificate=true");
            
        return new HeuteDbContext(optionsBuilder.Options);
    }
}