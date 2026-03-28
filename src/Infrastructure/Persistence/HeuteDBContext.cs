using Microsoft.EntityFrameworkCore;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Infrastructure.Models.Profile;
using HeuteApp.Infrastructure.Models.Layout;
using HeuteApp.Infrastructure.Models.Category;
using HeuteApp.Infrastructure.Models.Dailyboard;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{    
    public DbSet<HeuteProfileModel> Profiles => Set<HeuteProfileModel>();
    
    public DbSet<HeuteLayoutModel> Layouts => Set<HeuteLayoutModel>();

    public DbSet<HeuteCategoryModel> Categories => Set<HeuteCategoryModel>();

    public DbSet<HeuteDailyboardModel> Dailyboards => Set<HeuteDailyboardModel>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {    
        modelBuilder.Ignore<DailyboardCard>();
        modelBuilder.Ignore<LayoutSection>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeuteDbContext).Assembly);
    }

}