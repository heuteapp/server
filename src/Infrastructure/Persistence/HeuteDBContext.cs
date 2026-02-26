using Microsoft.EntityFrameworkCore;
using HeuteApp.Infrastructure.Persistence.Entities;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Infrastructure.Persistence;

public class HeuteDbContext(DbContextOptions<HeuteDbContext> options) : DbContext(options)
{
    public DbSet<BoardEntity> Boards => Set<BoardEntity>();

    public DbSet<BoardCardEntity> BoardCards => Set<BoardCardEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoardEntity>(builder =>
        {
            builder.ToTable("boards");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.OwnerId).IsRequired();
            builder.Property(b => b.LayoutId).IsRequired();
        });

        modelBuilder.Entity<BoardCardEntity>(builder =>
        {
            builder.ToTable("board_cards");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.BoardId).IsRequired();
            builder.Property(c => c.SectionId);
            builder.Property(c => c.Title).IsRequired();

            //builder.Property(c => c.Position);
        });

        modelBuilder.Entity<BoardCardEntity>()
            .OwnsOne(x => x.Position);
    }
}