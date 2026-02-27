using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models;

namespace HeuteApp.Infrastructure.Configurations;

public class LayoutConfig : IEntityTypeConfiguration<LayoutModel>
{
    public void Configure(EntityTypeBuilder<LayoutModel> builder)
    {
        builder.ToTable("layouts");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.OwnerId)
               .IsRequired();

        builder.Property(l => l.Name)
               .IsRequired();

        builder.Property(l => l.Version)
               .IsRequired();

        builder.HasMany(l => l.Sections)
               .WithOne(s => s.Layout)
               .HasForeignKey(s => s.LayoutId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(l => new { l.OwnerId, l.Name, l.Version })
               .IsUnique();
    }
}