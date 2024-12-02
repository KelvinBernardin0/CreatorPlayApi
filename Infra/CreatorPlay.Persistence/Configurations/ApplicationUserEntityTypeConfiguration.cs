using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id).IsClustered();
        builder.Property(b => b.CreatedAt).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired(true);
        builder.Property(b => b.ModifiedAt).UsePropertyAccessMode(PropertyAccessMode.Field).IsRequired(false);
    }

}
