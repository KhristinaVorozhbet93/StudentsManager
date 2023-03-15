using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingPassportForStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "Students",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passport",
                table: "Students");
        }
    }
}
