using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndexOfNameForStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_Name",
                table: "Students",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Name",
                table: "Students");
        }
    }
}
