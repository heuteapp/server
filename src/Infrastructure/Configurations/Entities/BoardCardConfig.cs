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

        builder.Property(c => c.SectionId)
                .IsRequired(false);

        builder.Property(c => c.Title)
               .IsRequired();

        builder.Ignore(c => c.IsVerified);
        builder.Ignore(c => c.HasPlacement);
        builder.Ignore(c => c.CanBePlaced);
        builder.Ignore(c => c.IsPlaced);


        builder.Property(c => c.Position)
                .HasColumnType("jsonb")
                .IsRequired(false);

        builder.HasIndex(c => c.BoardId);
    }
}