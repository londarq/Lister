using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("Test").HasKey(t => t.TestId);

        builder.Property(t => t.TestId)
            .HasColumnName("test_id");
        builder.Property(t => t.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.HasMany(t => t.Questions)
            .WithOne(q => q.Test)
            .HasForeignKey(q => q.TestID)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(t => t.UserTestHistory)
            .WithOne(uth => uth.Test)
            .HasForeignKey(uth => uth.TestID)
            .OnDelete(DeleteBehavior.SetNull);
    }

}
