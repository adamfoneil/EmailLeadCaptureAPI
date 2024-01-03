using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptStatus",
                table: "email_lead");

            migrationBuilder.RenameColumn(
                name: "DateCreatedUtc",
                table: "email_lead",
                newName: "CreatedUtc");

            migrationBuilder.RenameColumn(
                name: "ConfirmedDateUtc",
                table: "email_lead",
                newName: "ConfirmedUtc");

            migrationBuilder.AddColumn<bool>(
                name: "IsOptedIn",
                table: "email_lead",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOptedIn",
                table: "email_lead");

            migrationBuilder.RenameColumn(
                name: "CreatedUtc",
                table: "email_lead",
                newName: "DateCreatedUtc");

            migrationBuilder.RenameColumn(
                name: "ConfirmedUtc",
                table: "email_lead",
                newName: "ConfirmedDateUtc");

            migrationBuilder.AddColumn<int>(
                name: "OptStatus",
                table: "email_lead",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
