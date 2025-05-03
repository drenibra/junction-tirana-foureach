using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFMoneyMatters.Migrations
{
    /// <inheritdoc />
    public partial class updatedusermodel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Lessons_LessonId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_LessonId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Goals_PersonId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Lessons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "LessonQuizQuestions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PersonId",
                table: "Lessons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PersonId",
                table: "Goals",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Persons_PersonId",
                table: "Lessons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Persons_PersonId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_PersonId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Goals_PersonId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "LessonQuizQuestions");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Persons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_LessonId",
                table: "Persons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PersonId",
                table: "Goals",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Lessons_LessonId",
                table: "Persons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
