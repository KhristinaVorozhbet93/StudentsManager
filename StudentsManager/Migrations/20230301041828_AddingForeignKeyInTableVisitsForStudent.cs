using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingForeignKeyInTableVisitsForStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Visits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_StudentId",
                table: "Visits",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_StudentId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Visits");
        }
    }
}
