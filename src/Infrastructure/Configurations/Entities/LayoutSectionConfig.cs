using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Layout;

namespace HeuteApp.Infrastructure.Configurations.Entities;

public class LayoutSectionConfig : IEntityTypeConfiguration<LayoutSectionModel>
{
    public void Configure(EntityTypeBuilder<LayoutSectionModel> builder)
    {
        builder.ToTable("layout_sections");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.LayoutId)
            .IsRequired();

        builder.Property(s => s.Name)
            .IsRequired();

        builder.OwnsOne(s => s.Rect, rect =>
        {
            rect.WithOwner();

            rect.Property(r => r.X)
                .HasColumnName("Rect_X")
                .IsRequired();

            rect.Property(r => r.Y)
                .HasColumnName("Rect_Y")
                .IsRequired();

            rect.Property(r => r.Width)
                .HasColumnName("Rect_Width")
                .IsRequired();

            rect.Property(r => r.Height)
                .HasColumnName("Rect_Height")
                .IsRequired();
        });

        builder.OwnsOne(s => s.Size, size =>
        {
            size.WithOwner();

            size.Property(s => s.ColCount)
                .HasColumnName("Size_ColCount")
                .IsRequired();

            size.Property(s => s.RowCount)
                .HasColumnName("Size_RowCount")
                .IsRequired();
        });

        builder.HasIndex(s => s.Id)
            .IsUnique();

        builder.HasIndex(s => new { s.LayoutId, s.Name })
            .IsUnique();
    }
}