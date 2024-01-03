using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class AppTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_email_lead_Application_Email",
                table: "email_lead");

            migrationBuilder.DropColumn(
                name: "Application",
                table: "email_lead");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "email_lead",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_email_lead_ApplicationId_Email",
                table: "email_lead",
                columns: new[] { "ApplicationId", "Email" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_email_lead_ApplicationId_Email",
                table: "email_lead");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "email_lead");

            migrationBuilder.AddColumn<string>(
                name: "Application",
                table: "email_lead",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_email_lead_Application_Email",
                table: "email_lead",
                columns: new[] { "Application", "Email" });
        }
    }
}
