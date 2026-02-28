using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Aggregates;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Configurations.Aggregates;

public class BoardConfig : IEntityTypeConfiguration<HeuteBoardModel>
{
    public void Configure(EntityTypeBuilder<HeuteBoardModel> builder)
    {
        builder.ToTable("boards");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.OwnerId)
            .IsRequired();

        builder.Property(b => b.LayoutId)
            .IsRequired();

        builder.Property(b => b.Date)
            .IsRequired();

        builder.HasMany<BoardCardModel>("m_cards")
            .WithOne(c => c.Board)
            .HasForeignKey(c => c.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<HeuteLayoutModel>()
            .WithMany()
            .HasForeignKey(b => b.LayoutId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation("m_cards")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}