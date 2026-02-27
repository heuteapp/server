using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{
    public DbSet<HeuteBoardModel> Boards => Set<HeuteBoardModel>();

    public DbSet<LayoutModel> Layouts => Set<LayoutModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeuteDbContext).Assembly);
    }

}