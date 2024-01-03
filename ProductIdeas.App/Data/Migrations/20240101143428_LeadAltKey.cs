using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class LeadAltKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmailLeads_Application_Email",
                table: "EmailLeads",
                columns: new[] { "Application", "Email" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmailLeads_Application_Email",
                table: "EmailLeads");
        }
    }
}
