using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Category;

namespace HeuteApp.Infrastructure.Configurations.Category;

public class HeuteCategoryConfig : IEntityTypeConfiguration<HeuteCategoryModel>
{
    public void Configure(EntityTypeBuilder<HeuteCategoryModel> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(b => b.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.ParentId)
            .IsRequired(false);

        builder.Property(b => b.Name)
            .IsRequired();

        builder.HasOne(c => c.Profile)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}