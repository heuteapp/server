using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Dailyboard;

namespace HeuteApp.Infrastructure.Configurations.Dailyboard;

public class HeuteDailyboardConfig : IEntityTypeConfiguration<HeuteDailyboardModel>
{
    public void Configure(EntityTypeBuilder<HeuteDailyboardModel> builder)
    {
        builder.ToTable("dailyboards");

        builder.HasKey(b => b.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(b => b.UserId)
            .IsRequired();

        builder.Property(b => b.LayoutId)
            .IsRequired();

        builder.Property(b => b.CategoryId)
            .IsRequired();

        builder.Property(b => b.Date)
            .IsRequired();

        builder.HasOne(b => b.Profile)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Layout)
            .WithMany()
            .HasForeignKey(b => b.LayoutId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.UserId, b.Date })
            .IsUnique();
    }
}