using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(u => u.UserID);

            builder.Property(u => u.UserID)
                .HasColumnName("user_id");
            builder.Property(u => u.Username)
                .HasColumnName("username")
                .IsRequired();
            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.HasMany(u => u.UserAnswers)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserTests)
                .WithOne(uth => uth.User)
                .HasForeignKey(uth => uth.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
