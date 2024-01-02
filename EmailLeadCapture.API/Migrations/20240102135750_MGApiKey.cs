using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailLeadCapture.API.Migrations
{
    /// <inheritdoc />
    public partial class MGApiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedDateUtc",
                table: "email_lead",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailgunApiKey",
                table: "application",
                type: "character varying(65)",
                maxLength: 65,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedDateUtc",
                table: "email_lead");

            migrationBuilder.DropColumn(
                name: "MailgunApiKey",
                table: "application");
        }
    }
}
