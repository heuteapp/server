using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Configurations;

public class LayoutSectionConfig : IEntityTypeConfiguration<LayoutSectionModel>
{
    public void Configure(EntityTypeBuilder<LayoutSectionModel> builder)
    {
        builder.ToTable("layout_sections");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.LayoutId)
               .IsRequired();

        builder.Property(s => s.Name)
               .IsRequired();

        builder.HasIndex(s => s.LayoutId);

        // Rect Value Object
        builder.OwnsOne(s => s.Rect, rect =>
        {
            rect.Property(r => r.X)
                .HasColumnName("rect_x")
                .IsRequired();

            rect.Property(r => r.Y)
                .HasColumnName("rect_y")
                .IsRequired();

            rect.Property(r => r.Width)
                .HasColumnName("rect_width")
                .IsRequired();

            rect.Property(r => r.Height)
                .HasColumnName("rect_height")
                .IsRequired();
        });

        builder.Navigation(s => s.Rect)
               .IsRequired();

        builder.OwnsOne(s => s.Size, size =>
        {
            size.Property(sz => sz.ColCount)
                .HasColumnName("size_colCount")
                .IsRequired();

            size.Property(sz => sz.RowCount)
                .HasColumnName("size_rowCount")
                .IsRequired();
        });

        builder.Navigation(s => s.Size)
               .IsRequired();
    }
}