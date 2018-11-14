using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SRUK.Data.Migrations
{
    public partial class AddDeadlineCompletionDateCommentChangeSatusesToByteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Season",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Review",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Review",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Review",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "PaperVerison",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "PaperVerison",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Paper",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "PaperVerison");

            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "PaperVerison",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<short>(
                name: "Status",
                table: "Paper",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}
