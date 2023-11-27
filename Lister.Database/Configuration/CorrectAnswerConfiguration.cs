using Lister.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lister.Database.Configuration
{
    public class CorrectAnswerConfiguration : IEntityTypeConfiguration<CorrectAnswer>
    {
        public void Configure(EntityTypeBuilder<CorrectAnswer> builder)
        {
            builder.ToTable("CorrectAnswer").HasKey(ca => ca.CorrectAnswerID);

            builder.Property(ca => ca.CorrectAnswerID)
                .HasColumnName("correct_answer_id");
            builder.Property(ca => ca.AnswerID)
                .HasColumnName("answer_id");

            builder.HasOne(ca => ca.Answer)
                .WithOne(a => a.CorrectAnswer)
                .HasForeignKey<CorrectAnswer>(ca => ca.AnswerID);
        }
    }
}
