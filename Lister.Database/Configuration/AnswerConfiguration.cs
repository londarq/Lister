using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer").HasKey(a => a.AnswerID);

            builder.Property(a => a.AnswerID)
                .HasColumnName("answer_id");
            builder.Property(a => a.QuestionID)
                .HasColumnName("question_id");
            builder.Property(a => a.AnswerText)
                .HasColumnName("answer_text");

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionID);

            builder.HasOne(a => a.CorrectAnswer)
                .WithOne(ca => ca.Answer)
                .HasForeignKey<CorrectAnswer>(ca => ca.AnswerID);

            builder.HasMany(a => a.UserAnswers)
                .WithOne(ua => ua.SelectedAnswer)
                .HasForeignKey(ua => ua.SelectedAnswerID);
        }
    }
}
