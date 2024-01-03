using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameCols2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailgunApiKey",
                table: "application");

            migrationBuilder.RenameColumn(
                name: "OptStatusChangedUtc",
                table: "email_lead",
                newName: "OptChangedUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OptChangedUtc",
                table: "email_lead",
                newName: "OptStatusChangedUtc");

            migrationBuilder.AddColumn<string>(
                name: "MailgunApiKey",
                table: "application",
                type: "character varying(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "");
        }
    }
}
