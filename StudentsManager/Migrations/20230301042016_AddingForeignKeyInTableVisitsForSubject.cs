using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingForeignKeyInTableVisitsForSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Visits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_SubjectId",
                table: "Visits",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Subjects_SubjectId",
                table: "Visits",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Subjects_SubjectId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_SubjectId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Visits");
        }
    }
}
