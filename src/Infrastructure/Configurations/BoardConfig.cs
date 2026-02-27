using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Entities;

namespace HeuteApp.Infrastructure.Configurations;

public class BoardConfig : IEntityTypeConfiguration<BoardEntity>
{
    public void Configure(EntityTypeBuilder<BoardEntity> builder)
    {
        builder.ToTable("boards");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.OwnerId)
            .IsRequired();

        builder.Property(b => b.LayoutId)
            .IsRequired();

        builder.Property(b => b.Date)
            .IsRequired();

        builder.HasMany(b => b.Cards)
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<LayoutEntity>()
            .WithMany()
            .HasForeignKey(b => b.LayoutId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}