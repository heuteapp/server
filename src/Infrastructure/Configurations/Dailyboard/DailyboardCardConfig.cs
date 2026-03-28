using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Dailyboard;

namespace HeuteApp.Infrastructure.Configurations.Dailyboard;

public class DailyboardCardConfig : IEntityTypeConfiguration<DailyboardCardModel>
{
    public void Configure(EntityTypeBuilder<DailyboardCardModel> builder)
    {
        builder.ToTable("dailyboard_cards");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .ValueGeneratedNever();

        builder.Property(c => c.DailyboardId)
               .IsRequired();

        builder.Property(c => c.Name)
               .IsRequired();

        builder.OwnsOne(c => c.Content, content =>
        {
                content.Property(p => p.Title)
                        .HasColumnName("Content_Title")
                        .IsRequired();
        });

        builder.OwnsOne(c => c.Placement, placement =>
        {
                placement.Property(p => p.SectionName)
                        .HasColumnName("Placement_SectionName")
                        .IsRequired();

                placement.Property(p => p.ColIndex)
                        .HasColumnName("Placement_ColIndex");
                
                placement.Property(p => p.RowIndex)
                        .HasColumnName("Placement_RowIndex");

                placement.Property(p => p.ColSpan)
                        .HasColumnName("Placement_ColSpan");
                
                placement.Property(p => p.RowSpan)
                        .HasColumnName("Placement_RowSpan");

                placement.Ignore(p => p.Section);
                placement.Ignore(p => p.Position);
        });

        builder.Navigation(c => c.Placement)
               .IsRequired(false);
               
        builder.Ignore(c => c.IsPlaced);

        builder.HasIndex(c => c.DailyboardId);

        
        builder.HasOne(c => c.Dailyboard)
            .WithMany(b => (IEnumerable<DailyboardCardModel>)b.Cards)
            .HasForeignKey(c => c.DailyboardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}