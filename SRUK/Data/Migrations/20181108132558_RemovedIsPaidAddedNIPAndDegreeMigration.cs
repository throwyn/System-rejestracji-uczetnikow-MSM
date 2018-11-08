using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SRUK.Data.Migrations
{
    public partial class RemovedIsPaidAddedNIPAndDegreeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Paper");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VATID",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VATID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Paper",
                nullable: false,
                defaultValue: false);
        }
    }
}
