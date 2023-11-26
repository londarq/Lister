using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable("UserAnswer").HasKey(ua => ua.UserAnswerID);

            builder.Property(ua => ua.UserAnswerID)
                .HasColumnName("user_answer_id");
            builder.Property(ua => ua.UserID)
                .HasColumnName("user_id");
            builder.Property(ua => ua.QuestionID)
                .HasColumnName("question_id");
            builder.Property(ua => ua.SelectedAnswerID)
                .HasColumnName("selected_answer_id");

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAnswers)
                .HasForeignKey(ua => ua.UserID);

            builder.HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionID);

            builder.HasOne(ua => ua.SelectedAnswer)
                .WithMany(a => a.UserAnswers)
                .HasForeignKey(ua => ua.SelectedAnswerID);
        }
    }
}
