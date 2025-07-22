using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens"); // <-- Set plural table name here

            builder.HasKey(rt => rt.TokenId); 

            builder.Property(rt => rt.Token)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(rt => rt.UserId)
                   .IsRequired();

            builder.Property(rt => rt.ExpiresOn)
                   .IsRequired();

            builder.Property(rt => rt.CreatedOn)
                   .IsRequired();

            builder.Property(rt => rt.RevokedOn)
                   .IsRequired(false); // Nullable

            builder.Ignore(rt => rt.IsExpired); // computed
            builder.Ignore(rt => rt.IsActive);  // computed

            // Optional: index for fast lookups
            builder.HasIndex(rt => rt.UserId);
        }
    }

}
