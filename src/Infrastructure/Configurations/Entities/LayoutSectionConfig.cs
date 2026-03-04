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

        builder.OwnsOne(s => s.Area, area =>
        {
            area.WithOwner();

            area.Property(r => r.Col)
                .HasColumnName("Area_Col")
                .IsRequired();

            area.Property(r => r.Row)
                .HasColumnName("Area_Row")
                .IsRequired();

            area.Property(r => r.ColSpan)
                .HasColumnName("Area_ColSpan")
                .IsRequired();

            area.Property(r => r.RowSpan)
                .HasColumnName("Area_RowSpan")
                .IsRequired();
        });

        builder.HasIndex(s => new { s.LayoutId, s.Name })
            .IsUnique();
    }
}