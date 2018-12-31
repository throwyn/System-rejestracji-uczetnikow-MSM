using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SRUK.Data.Migrations
{
    public partial class SeasonEditedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoFileName",
                table: "Season",
                newName: "Location");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConferenceEndDate",
                table: "Season",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ConferenceStartDate",
                table: "Season",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EditionNumber",
                table: "Season",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConferenceEndDate",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "ConferenceStartDate",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "EditionNumber",
                table: "Season");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Season",
                newName: "LogoFileName");
        }
    }
}
