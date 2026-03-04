using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Layout;

namespace HeuteApp.Infrastructure.Configurations.Layout;

public class HeuteLayoutConfig : IEntityTypeConfiguration<HeuteLayoutModel>
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

       builder.HasOne(l => l.Owner)
              .WithMany()
              .HasForeignKey(l => l.OwnerId)
              .OnDelete(DeleteBehavior.Restrict);

       builder.HasIndex(l => new { l.OwnerId, l.Name, l.Version })
              .IsUnique();
    }
}