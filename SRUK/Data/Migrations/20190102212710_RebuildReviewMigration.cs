using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SRUK.Data.Migrations
{
    public partial class RebuildReviewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorialErrors",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "IsPositive",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "RepeatReview",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "TechnicalErrors",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "Unsuitable",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Review",
                newName: "Recommendation");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Review",
                newName: "CommentForAuthor");

            migrationBuilder.AddColumn<string>(
                name: "CommentForAdmin",
                table: "Review",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentForAdmin",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "Recommendation",
                table: "Review",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CommentForAuthor",
                table: "Review",
                newName: "Comment");

            migrationBuilder.AddColumn<bool>(
                name: "EditorialErrors",
                table: "Review",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPositive",
                table: "Review",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RepeatReview",
                table: "Review",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TechnicalErrors",
                table: "Review",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Unsuitable",
                table: "Review",
                nullable: true);
        }
    }
}
