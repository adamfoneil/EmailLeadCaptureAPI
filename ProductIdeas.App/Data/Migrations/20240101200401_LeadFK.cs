using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class LeadFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_application_Name",
                table: "application",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_email_lead_application_ApplicationId",
                table: "email_lead",
                column: "ApplicationId",
                principalTable: "application",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_email_lead_application_ApplicationId",
                table: "email_lead");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_application_Name",
                table: "application");
        }
    }
}
