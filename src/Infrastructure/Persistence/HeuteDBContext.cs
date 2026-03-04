using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Board;
using HeuteApp.Infrastructure.Models.Category;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{
    public DbSet<HeuteBoardModel> Boards => Set<HeuteBoardModel>();

    public DbSet<HeuteLayoutModel> Layouts => Set<HeuteLayoutModel>();

    public DbSet<HeuteCategoryModel> Categories => Set<HeuteCategoryModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {    
        modelBuilder.Ignore<BoardCard>();
        modelBuilder.Ignore<LayoutSection>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeuteDbContext).Assembly);
    }

}