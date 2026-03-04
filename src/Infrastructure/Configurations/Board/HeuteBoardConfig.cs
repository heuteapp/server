using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Board;

namespace HeuteApp.Infrastructure.Configurations.Board;

public class HeuteBoardConfig : IEntityTypeConfiguration<HeuteBoardModel>
{
    public void Configure(EntityTypeBuilder<HeuteBoardModel> builder)
    {
        builder.ToTable("boards");

        builder.HasKey(b => b.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(b => b.OwnerId)
            .IsRequired();

        builder.Property(b => b.LayoutId)
            .IsRequired();

        builder.Property(b => b.CategoryId)
            .IsRequired();

        builder.Property(b => b.Date)
            .IsRequired();

        builder.HasMany(b => (IEnumerable<BoardCardModel>)b.Cards)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Layout)
            .WithMany()
            .HasForeignKey(b => b.LayoutId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => b.Id)
            .IsUnique();

        builder.HasIndex(b => new { b.OwnerId, b.Date });
    }
}