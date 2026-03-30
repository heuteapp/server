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

       builder.Property(l => l.UserId)
              .IsRequired(false);

       builder.Property(l => l.Name)
              .IsRequired();

       builder.Property(l => l.Version)
              .IsRequired();

       builder.OwnsOne(l => l.Dimensions, size =>
       {
           size.Property(s => s.ColCount)
              .HasColumnName("Dimensions_ColCount")
              .IsRequired();

           size.Property(s => s.RowCount)
              .HasColumnName("Dimensions_RowCount")
              .IsRequired();
       });

       builder.HasOne(l => l.Profile)
              .WithMany()
              .HasForeignKey(l => l.UserId)
              .OnDelete(DeleteBehavior.Restrict);

       builder.HasIndex(l => new { l.UserId, l.Name, l.Version })
              .IsUnique();
    }
}