using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmailLead_Application_Email",
                table: "EmailLead");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLead",
                table: "EmailLead");

            migrationBuilder.RenameTable(
                name: "EmailLead",
                newName: "email_lead");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_email_lead_Application_Email",
                table: "email_lead",
                columns: new[] { "Application", "Email" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_email_lead",
                table: "email_lead",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_email_lead_Application_Email",
                table: "email_lead");

            migrationBuilder.DropPrimaryKey(
                name: "PK_email_lead",
                table: "email_lead");

            migrationBuilder.RenameTable(
                name: "email_lead",
                newName: "EmailLead");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmailLead_Application_Email",
                table: "EmailLead",
                columns: new[] { "Application", "Email" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailLead",
                table: "EmailLead",
                column: "Id");
        }
    }
}
