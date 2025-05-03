using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RFMoneyMatters.Migrations
{
    /// <inheritdoc />
    public partial class addedquizmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Persons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LessonQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonQuizzes_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonQuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    A = table.Column<string>(type: "text", nullable: false),
                    B = table.Column<string>(type: "text", nullable: false),
                    C = table.Column<string>(type: "text", nullable: true),
                    D = table.Column<string>(type: "text", nullable: true),
                    RightAnswer = table.Column<string>(type: "text", nullable: false),
                    LessonQuizId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonQuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonQuizQuestions_LessonQuizzes_LessonQuizId",
                        column: x => x.LessonQuizId,
                        principalTable: "LessonQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonQuizResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    LessonQuizId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonQuizResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonQuizResults_LessonQuizzes_LessonQuizId",
                        column: x => x.LessonQuizId,
                        principalTable: "LessonQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonQuizResults_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_LessonId",
                table: "Persons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuizQuestions_LessonQuizId",
                table: "LessonQuizQuestions",
                column: "LessonQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuizResults_LessonQuizId",
                table: "LessonQuizResults",
                column: "LessonQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuizResults_PersonId",
                table: "LessonQuizResults",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuizzes_LessonId",
                table: "LessonQuizzes",
                column: "LessonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Lessons_LessonId",
                table: "Persons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Lessons_LessonId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "LessonQuizQuestions");

            migrationBuilder.DropTable(
                name: "LessonQuizResults");

            migrationBuilder.DropTable(
                name: "LessonQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_Persons_LessonId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Persons");
        }
    }
}
