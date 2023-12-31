﻿// <auto-generated />
using System;
using Lister.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lister.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231128151658_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lister.Core.Models.Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("answer_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerID"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("answer_text");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int")
                        .HasColumnName("question_id");

                    b.HasKey("AnswerID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.CorrectAnswer", b =>
                {
                    b.Property<int>("CorrectAnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("correct_answer_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CorrectAnswerID"));

                    b.Property<int>("AnswerID")
                        .HasColumnType("int")
                        .HasColumnName("answer_id");

                    b.HasKey("CorrectAnswerID");

                    b.HasIndex("AnswerID")
                        .IsUnique();

                    b.ToTable("CorrectAnswer", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("question_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionID"));

                    b.Property<string>("MediaLink")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("media_link");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("question_text");

                    b.Property<int>("TestID")
                        .HasColumnType("int")
                        .HasColumnName("test_id");

                    b.HasKey("QuestionID");

                    b.HasIndex("TestID");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("test_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("TimeLimitSec")
                        .HasColumnType("int");

                    b.HasKey("TestId");

                    b.ToTable("Test", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nickname");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("password_salt");

                    b.HasKey("UserID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.UserAnswer", b =>
                {
                    b.Property<int>("UserAnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_answer_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAnswerID"));

                    b.Property<int>("SelectedAnswerID")
                        .HasColumnType("int")
                        .HasColumnName("selected_answer_id");

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("UserAnswerID");

                    b.HasIndex("SelectedAnswerID");

                    b.HasIndex("UserID");

                    b.ToTable("UserAnswer", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.UserTestHistory", b =>
                {
                    b.Property<int>("UserTestHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_test_history_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTestHistoryID"));

                    b.Property<DateTime>("FinishTimestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("finish_timestamp");

                    b.Property<int>("Score")
                        .HasColumnType("int")
                        .HasColumnName("score");

                    b.Property<DateTime>("StartTimestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_timestamp");

                    b.Property<int?>("TestID")
                        .HasColumnType("int")
                        .HasColumnName("test_id");

                    b.Property<int>("UserID")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("UserTestHistoryID");

                    b.HasIndex("TestID");

                    b.HasIndex("UserID");

                    b.ToTable("UserTestHistory", (string)null);
                });

            modelBuilder.Entity("Lister.Core.Models.Answer", b =>
                {
                    b.HasOne("Lister.Core.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Lister.Core.Models.CorrectAnswer", b =>
                {
                    b.HasOne("Lister.Core.Models.Answer", "Answer")
                        .WithOne("CorrectAnswer")
                        .HasForeignKey("Lister.Core.Models.CorrectAnswer", "AnswerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Answer");
                });

            modelBuilder.Entity("Lister.Core.Models.Question", b =>
                {
                    b.HasOne("Lister.Core.Models.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Lister.Core.Models.UserAnswer", b =>
                {
                    b.HasOne("Lister.Core.Models.Answer", "SelectedAnswer")
                        .WithMany("UserAnswers")
                        .HasForeignKey("SelectedAnswerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lister.Core.Models.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SelectedAnswer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lister.Core.Models.UserTestHistory", b =>
                {
                    b.HasOne("Lister.Core.Models.Test", "Test")
                        .WithMany("UserTestHistory")
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Lister.Core.Models.User", "User")
                        .WithMany("UserTests")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lister.Core.Models.Answer", b =>
                {
                    b.Navigation("CorrectAnswer");

                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("Lister.Core.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Lister.Core.Models.Test", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("UserTestHistory");
                });

            modelBuilder.Entity("Lister.Core.Models.User", b =>
                {
                    b.Navigation("UserAnswers");

                    b.Navigation("UserTests");
                });
#pragma warning restore 612, 618
        }
    }
}
