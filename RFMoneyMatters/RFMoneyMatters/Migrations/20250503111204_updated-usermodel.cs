using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFMoneyMatters.Migrations
{
    /// <inheritdoc />
    public partial class updatedusermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClerckId",
                table: "Persons",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClerckId",
                table: "Persons");
        }
    }
}
