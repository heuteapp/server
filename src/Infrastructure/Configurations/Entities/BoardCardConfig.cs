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
            placement.WithOwner();

            placement.OwnsOne(p => p.Section, section =>
            {
                section.Property(s => s.Name)
                       .HasColumnName("Section_Name")
                       .IsRequired();
            });

            placement.OwnsOne(p => p.Position, position =>
            {
                position.Property(p => p.Col)
                        .HasColumnName("Position_Col")
                        .IsRequired();

                position.Property(p => p.Row)
                        .HasColumnName("Position_Row")
                        .IsRequired();

                position.Property(p => p.ColSpan)
                        .HasColumnName("Position_ColSpan")
                        .IsRequired();

                position.Property(p => p.RowSpan)
                        .HasColumnName("Position_RowSpan")
                        .IsRequired();
            });
        });

        builder.Ignore(c => c.IsVerified);
        builder.Ignore(c => c.HasPlacement);
        builder.Ignore(c => c.CanBePlaced);
        builder.Ignore(c => c.IsPlaced);

        builder.HasIndex(c => c.BoardId);
    }
}