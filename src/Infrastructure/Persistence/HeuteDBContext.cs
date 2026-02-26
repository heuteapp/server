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

            builder.HasMany(b => b.Cards)
                .WithOne(c => c.Board)
                .HasForeignKey(c => c.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BoardCardEntity>(builder =>
        {
            builder.ToTable("board_cards");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.BoardId).IsRequired();
            builder.Property(c => c.SectionId);
            builder.Property(c => c.Title).IsRequired();

            builder.HasIndex(c => c.BoardId);
        });

        modelBuilder.Entity<BoardCardEntity>()
            .OwnsOne(x => x.Position);

        modelBuilder.Entity<LayoutEntity>(builder =>
        {
            builder.ToTable("layouts");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.OwnerId).IsRequired();
            builder.Property(l => l.Name).IsRequired();
            builder.Property(l => l.Version).IsRequired();

            builder.HasMany(l => l.Sections)
                .WithOne(s => s.Layout)
                .HasForeignKey(s => s.LayoutId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LayoutSectionEntity>(builder =>
        {
            builder.ToTable("layout_sections");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.LayoutId).IsRequired();
            builder.Property(s => s.Name).IsRequired();

            builder.HasIndex(s => s.LayoutId);
        });

        modelBuilder.Entity<LayoutSectionEntity>()
            .OwnsOne(x => x.Rect);

        modelBuilder.Entity<LayoutSectionEntity>()
            .OwnsOne(x => x.Size);
    }
}