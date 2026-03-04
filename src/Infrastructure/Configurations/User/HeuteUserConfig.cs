using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeuteApp.Infrastructure.Models.User;

namespace HeuteApp.Infrastructure.Configurations.User;

public class HeuteUserConfig : IEntityTypeConfiguration<HeuteUserModel>
{
    public void Configure(EntityTypeBuilder<HeuteUserModel> builder)
    {
        builder.ToTable("users");

        builder.HasKey(b => b.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(b => b.Name)
            .IsRequired();
    }
}