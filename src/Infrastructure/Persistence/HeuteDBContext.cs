using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Persistence.Entities;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{
    public DbSet<BoardEntity> Boards => Set<BoardEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoardEntity>(builder =>
        {
            builder.ToTable("boards");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.OwnerId).IsRequired();
            builder.Property(b => b.LayoutId).IsRequired();
        });
    }
}