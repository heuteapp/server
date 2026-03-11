using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.Profile;

namespace HeuteApp.Infrastructure.Configurations.Profile;

public class HeuteProfileConfig : IEntityTypeConfiguration<HeuteProfileModel>
{
    public void Configure(EntityTypeBuilder<HeuteProfileModel> builder)
    {
        builder.ToTable("profiles");

        builder.HasKey(b => b.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(b => b.Name)
            .IsRequired();

        builder.Property(b => b.Email)
            .IsRequired();
    }
}