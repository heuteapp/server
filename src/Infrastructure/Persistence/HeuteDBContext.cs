using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Entities;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{
    public DbSet<BoardEntity> Boards => Set<BoardEntity>();

    public DbSet<BoardCardEntity> BoardCards => Set<BoardCardEntity>();

    public DbSet<LayoutEntity> Layouts => Set<LayoutEntity>();

    public DbSet<LayoutSectionEntity> LayoutSections => Set<LayoutSectionEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeuteDbContext).Assembly);
    }

}