using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SRUK.Data.Migrations
{
    public partial class AdedReviewsToPaperVersionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaperVersion_Paper_PaperId",
                table: "PaperVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_PaperVersion_PaperVersionId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaperVersion",
                table: "PaperVersion");

            migrationBuilder.RenameTable(
                name: "PaperVersion",
                newName: "PaperVerison");

            migrationBuilder.RenameIndex(
                name: "IX_PaperVersion_PaperId",
                table: "PaperVerison",
                newName: "IX_PaperVerison_PaperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaperVerison",
                table: "PaperVerison",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaperVerison_Paper_PaperId",
                table: "PaperVerison",
                column: "PaperId",
                principalTable: "Paper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_PaperVerison_PaperVersionId",
                table: "Review",
                column: "PaperVersionId",
                principalTable: "PaperVerison",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaperVerison_Paper_PaperId",
                table: "PaperVerison");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_PaperVerison_PaperVersionId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaperVerison",
                table: "PaperVerison");

            migrationBuilder.RenameTable(
                name: "PaperVerison",
                newName: "PaperVersion");

            migrationBuilder.RenameIndex(
                name: "IX_PaperVerison_PaperId",
                table: "PaperVersion",
                newName: "IX_PaperVersion_PaperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaperVersion",
                table: "PaperVersion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaperVersion_Paper_PaperId",
                table: "PaperVersion",
                column: "PaperId",
                principalTable: "Paper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_PaperVersion_PaperVersionId",
                table: "Review",
                column: "PaperVersionId",
                principalTable: "PaperVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
