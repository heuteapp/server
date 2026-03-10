using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Infrastructure.Models.Board;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{    
    public DbSet<HeuteProfileModel> Profiles => Set<HeuteProfileModel>();
    
    public DbSet<HeuteLayoutModel> Layouts => Set<HeuteLayoutModel>();

    public DbSet<HeuteCategoryModel> Categories => Set<HeuteCategoryModel>();

    public DbSet<HeuteBoardModel> Boards => Set<HeuteBoardModel>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {    
        modelBuilder.Ignore<BoardCard>();
        modelBuilder.Ignore<LayoutSection>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeuteDbContext).Assembly);
    }

}