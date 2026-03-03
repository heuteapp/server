using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Board;

namespace HeuteApp.Infrastructure.Configurations.Entities;

public class BoardCardConfig : IEntityTypeConfiguration<BoardCardModel>
{
    public void Configure(EntityTypeBuilder<BoardCardModel> builder)
    {
        builder.ToTable("board_cards");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .ValueGeneratedNever();

        builder.Property(c => c.BoardId)
               .IsRequired();

        builder.Property(c => c.Title)
               .IsRequired();

        builder.OwnsOne(c => c.Placement, placement =>
        {
                placement.Property(p => p.SectionName)
                        .HasColumnName("Placement_SectionName")
                        .IsRequired();

                placement.Property(p => p.Col)
                        .HasColumnName("Placement_Col");
                
                placement.Property(p => p.Row)
                        .HasColumnName("Placement_Row");

                placement.Property(p => p.ColSpan)
                        .HasColumnName("Placement_ColSpan");
                
                placement.Property(p => p.RowSpan)
                        .HasColumnName("Placement_RowSpan");

                placement.Ignore(p => p.Section);
                placement.Ignore(p => p.Position);
        });

        builder.Navigation(c => c.Placement)
               .IsRequired(false);

        builder.Ignore(c => c.IsVerified);
        builder.Ignore(c => c.HasPlacement);
        builder.Ignore(c => c.CanBePlaced);
        builder.Ignore(c => c.IsPlaced);

        builder.HasIndex(c => c.BoardId);
    }
}