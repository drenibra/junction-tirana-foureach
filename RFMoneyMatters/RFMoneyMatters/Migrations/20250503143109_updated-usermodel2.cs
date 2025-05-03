using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFMoneyMatters.Migrations
{
    /// <inheritdoc />
    public partial class updatedusermodel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Persons_PersonId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_PersonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Lessons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Lessons",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PersonId",
                table: "Lessons",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Persons_PersonId",
                table: "Lessons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
