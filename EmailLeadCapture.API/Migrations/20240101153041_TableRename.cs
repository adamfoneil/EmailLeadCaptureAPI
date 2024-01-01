using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class TableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmailLeads_Application_Email",
                table: "EmailLeads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLeads",
                table: "EmailLeads");

            migrationBuilder.RenameTable(
                name: "EmailLeads",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmailLead_Application_Email",
                table: "EmailLead");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLead",
                table: "EmailLead");

            migrationBuilder.RenameTable(
                name: "EmailLead",
                newName: "EmailLeads");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmailLeads_Application_Email",
                table: "EmailLeads",
                columns: new[] { "Application", "Email" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailLeads",
                table: "EmailLeads",
                column: "Id");
        }
    }
}
