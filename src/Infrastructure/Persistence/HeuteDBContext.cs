using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Models.Aggregates;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{
    public DbSet<HeuteBoardModel> Boards => Set<HeuteBoardModel>();

    public DbSet<HeuteLayoutModel> Layouts => Set<HeuteLayoutModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {    
        modelBuilder.Ignore<BoardCard>();
        modelBuilder.Ignore<LayoutSection>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeuteDbContext).Assembly);
    }

}