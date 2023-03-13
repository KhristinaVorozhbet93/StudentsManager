using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndexOfNameForSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_Name",
                table: "Subjects");
        }
    }
}
