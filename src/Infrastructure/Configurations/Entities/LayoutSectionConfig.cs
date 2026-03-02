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

        builder.Property(s => s.LayoutId)
               .IsRequired();

        builder.Property(s => s.Name)
               .IsRequired();

        builder.HasIndex(s => s.LayoutId);

        builder.Property(c => c.Rect)
            .HasColumnType("jsonb");

        builder.Property(c => c.Size)
            .HasColumnType("jsonb");
    }
}