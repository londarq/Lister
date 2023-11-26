using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question").HasKey(q => q.QuestionID);

            builder.Property(q => q.QuestionID)
                .HasColumnName("question_id");
            builder.Property(q => q.TestID)
                .HasColumnName("test_id");
            builder.Property(q => q.QuestionText)
                .HasColumnName("question_text");
            builder.Property(q => q.MediaLink)
                .HasColumnName("media_link");

            builder.HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestID);
            
            builder.HasMany(q => q.Answers)
                .WithOne(ca => ca.Question)
                .HasForeignKey(ca => ca.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.CorrectAnswers)
                .WithOne(ca => ca.Question)
                .HasForeignKey(ca => ca.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.UserAnswers)
                .WithOne(ua => ua.Question)
                .HasForeignKey(ua => ua.QuestionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
