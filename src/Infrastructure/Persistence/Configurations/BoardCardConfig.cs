using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Persistence.Entities;

namespace HeuteApp.Infrastructure.Persistence.Configurations;

public class BoardCardConfig : IEntityTypeConfiguration<BoardCardEntity>
{
    public void Configure(EntityTypeBuilder<BoardCardEntity> builder)
    {
        builder.ToTable("board_cards");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.BoardId)
               .IsRequired();

        builder.Property(c => c.SectionId);

        builder.Property(c => c.Title)
               .IsRequired();

        builder.OwnsOne(c => c.Position, position =>
        {
            position.Property(p => p.Col)
                    .HasColumnName("position_col")
                    .IsRequired();

            position.Property(p => p.Row)
                    .HasColumnName("position_row")
                    .IsRequired();

            position.Property(p => p.ColSpan)
                    .HasColumnName("position_colSpan")
                    .IsRequired();

            position.Property(p => p.RowSpan)
                    .HasColumnName("position_rowSpan")
                    .IsRequired();
        });

        builder.Navigation(c => c.Position)
               .IsRequired(false);

        builder.HasIndex(c => c.BoardId);
    }
}