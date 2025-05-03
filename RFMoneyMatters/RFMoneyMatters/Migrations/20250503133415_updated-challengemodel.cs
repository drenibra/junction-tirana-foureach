using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFMoneyMatters.Migrations
{
    /// <inheritdoc />
    public partial class updatedchallengemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChallengeDate",
                table: "ChallengeDefinitions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ChallengeDate",
                table: "ChallengeDefinitions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
