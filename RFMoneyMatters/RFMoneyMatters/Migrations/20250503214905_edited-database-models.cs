using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFMoneyMatters.Migrations
{
    /// <inheritdoc />
    public partial class editeddatabasemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_PersonId1",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_AspNetUsers_PersonId1",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonQuizResults_AspNetUsers_PersonId1",
                table: "LessonQuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_AspNetUsers_PersonId1",
                table: "Questionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenges_AspNetUsers_PersonId1",
                table: "UserChallenges");

            migrationBuilder.DropIndex(
                name: "IX_UserChallenges_PersonId1",
                table: "UserChallenges");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaires_PersonId1",
                table: "Questionnaires");

            migrationBuilder.DropIndex(
                name: "IX_LessonQuizResults_PersonId1",
                table: "LessonQuizResults");

            migrationBuilder.DropIndex(
                name: "IX_Goals_PersonId1",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PersonId1",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Questionnaires");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "LessonQuizResults");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "UserChallenges",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Questionnaires",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "LessonQuizResults",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedAt",
                table: "LessonQuizResults",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "LessonQuizResults",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Goals",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Expenses",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_PersonId",
                table: "UserChallenges",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_PersonId",
                table: "Questionnaires",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuizResults_PersonId",
                table: "LessonQuizResults",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PersonId",
                table: "Goals",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PersonId",
                table: "Expenses",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_PersonId",
                table: "Expenses",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_AspNetUsers_PersonId",
                table: "Goals",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuizResults_AspNetUsers_PersonId",
                table: "LessonQuizResults",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_AspNetUsers_PersonId",
                table: "Questionnaires",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenges_AspNetUsers_PersonId",
                table: "UserChallenges",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_PersonId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_AspNetUsers_PersonId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonQuizResults_AspNetUsers_PersonId",
                table: "LessonQuizResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Questionnaires_AspNetUsers_PersonId",
                table: "Questionnaires");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChallenges_AspNetUsers_PersonId",
                table: "UserChallenges");

            migrationBuilder.DropIndex(
                name: "IX_UserChallenges_PersonId",
                table: "UserChallenges");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaires_PersonId",
                table: "Questionnaires");

            migrationBuilder.DropIndex(
                name: "IX_LessonQuizResults_PersonId",
                table: "LessonQuizResults");

            migrationBuilder.DropIndex(
                name: "IX_Goals_PersonId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PersonId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "LessonQuizResults");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "UserChallenges",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "UserChallenges",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Questionnaires",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Questionnaires",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "LessonQuizResults",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedAt",
                table: "LessonQuizResults",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "LessonQuizResults",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Goals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Expenses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Expenses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_PersonId1",
                table: "UserChallenges",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_PersonId1",
                table: "Questionnaires",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuizResults_PersonId1",
                table: "LessonQuizResults",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_PersonId1",
                table: "Goals",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PersonId1",
                table: "Expenses",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_PersonId1",
                table: "Expenses",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_AspNetUsers_PersonId1",
                table: "Goals",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuizResults_AspNetUsers_PersonId1",
                table: "LessonQuizResults",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questionnaires_AspNetUsers_PersonId1",
                table: "Questionnaires",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChallenges_AspNetUsers_PersonId1",
                table: "UserChallenges",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
