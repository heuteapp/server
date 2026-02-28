using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Aggregates;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Configurations.Aggregates;

public class LayoutConfig : IEntityTypeConfiguration<HeuteLayoutModel>
{
    public void Configure(EntityTypeBuilder<HeuteLayoutModel> builder)
    {
       builder.ToTable("layouts");

       builder.HasKey(l => l.Id);

       builder.Property(l => l.OwnerId)
              .IsRequired();

       builder.Property(l => l.Name)
              .IsRequired();

       builder.Property(l => l.Version)
              .IsRequired();

        builder.HasMany<LayoutSectionModel>("m_sections")
              .WithOne(s => s.Layout)
              .HasForeignKey(s => s.LayoutId)
              .OnDelete(DeleteBehavior.Cascade);

       builder.HasIndex(l => new { l.OwnerId, l.Name, l.Version })
              .IsUnique();

       builder.Navigation("m_sections")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}