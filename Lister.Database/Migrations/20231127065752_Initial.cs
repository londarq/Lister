using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lister.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    test_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLimitSec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.test_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test_id = table.Column<int>(type: "int", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    media_link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_Question_Test_test_id",
                        column: x => x.test_id,
                        principalTable: "Test",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTestHistory",
                columns: table => new
                {
                    user_test_history_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    test_id = table.Column<int>(type: "int", nullable: true),
                    start_timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finish_timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestHistory", x => x.user_test_history_id);
                    table.ForeignKey(
                        name: "FK_UserTestHistory_Test_test_id",
                        column: x => x.test_id,
                        principalTable: "Test",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserTestHistory_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    answer_text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_question_id",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectAnswer",
                columns: table => new
                {
                    correct_answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    answer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectAnswer", x => x.correct_answer_id);
                    table.ForeignKey(
                        name: "FK_CorrectAnswer_Answer_answer_id",
                        column: x => x.answer_id,
                        principalTable: "Answer",
                        principalColumn: "answer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    user_answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    selected_answer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.user_answer_id);
                    table.ForeignKey(
                        name: "FK_UserAnswer_Answer_selected_answer_id",
                        column: x => x.selected_answer_id,
                        principalTable: "Answer",
                        principalColumn: "answer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswer_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_question_id",
                table: "Answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswer_answer_id",
                table: "CorrectAnswer",
                column: "answer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_test_id",
                table: "Question",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_selected_answer_id",
                table: "UserAnswer",
                column: "selected_answer_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_user_id",
                table: "UserAnswer",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestHistory_test_id",
                table: "UserTestHistory",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestHistory_user_id",
                table: "UserTestHistory",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectAnswer");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "UserTestHistory");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
