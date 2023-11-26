using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration
{
    public class UserTestHistoryConfiguration : IEntityTypeConfiguration<UserTestHistory>
    {
        public void Configure(EntityTypeBuilder<UserTestHistory> builder)
        {
            builder.ToTable("UserTestHistory").HasKey(uth => uth.UserTestHistoryID);

            builder.Property(uth => uth.UserTestHistoryID)
                .HasColumnName("user_test_history_id");
            builder.Property(uth => uth.UserID)
                .HasColumnName("user_id");
            builder.Property(uth => uth.TestID)
                .HasColumnName("test_id");
            builder.Property(uth => uth.StartTimestamp)
                .HasColumnName("start_timestamp");
            builder.Property(uth => uth.FinishTimestamp)
                .HasColumnName("finish_timestamp");
            builder.Property(uth => uth.Score)
                .HasColumnName("score");

            builder.HasOne(uth => uth.User)
                .WithMany(u => u.UserTests)
                .HasForeignKey(uth => uth.UserID);

            builder.HasOne(uth => uth.Test)
                .WithMany(t => t.UserTestHistory)
                .HasForeignKey(uth => uth.TestID);
        }
    }
}
