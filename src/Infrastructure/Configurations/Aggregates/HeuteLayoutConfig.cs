using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Layout;

namespace HeuteApp.Infrastructure.Configurations.Aggregates;

public class LayoutConfig : IEntityTypeConfiguration<HeuteLayoutModel>
{
    public void Configure(EntityTypeBuilder<HeuteLayoutModel> builder)
    {
       builder.ToTable("layouts");

       builder.HasKey(l => l.Id);

       builder.Property(c => c.Id)
              .ValueGeneratedNever();

       builder.Property(l => l.OwnerId)
              .IsRequired();

       builder.Property(l => l.Name)
              .IsRequired();

       builder.Property(l => l.Version)
              .IsRequired();

       builder.OwnsOne(l => l.Size, size =>
       {
           size.Property(s => s.ColCount)
              .HasColumnName("Size_ColCount")
              .IsRequired();

           size.Property(s => s.RowCount)
              .HasColumnName("Size_RowCount")
              .IsRequired();
       });

       builder.HasMany(l => (IEnumerable<LayoutSectionModel>)l.Sections)
              .WithOne(s => s.Layout)
              .HasForeignKey(s => s.LayoutId)
              .OnDelete(DeleteBehavior.Cascade);

       builder.HasIndex(l => l.Id)
              .IsUnique();

       builder.HasIndex(l => new { l.OwnerId, l.Name, l.Version })
              .IsUnique();
    }
}