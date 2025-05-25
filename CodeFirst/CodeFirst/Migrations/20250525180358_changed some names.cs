using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class changedsomenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Patient",
                newName: "Birthdate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Medicament",
                newName: "Details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Patient",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Medicament",
                newName: "Description");
        }
    }
}
