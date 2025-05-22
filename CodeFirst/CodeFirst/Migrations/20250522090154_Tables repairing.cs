using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class Tablesrepairing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Medicament_Prescription_Medicament_PrescriptionMedicamentIdPrescription_PrescriptionMedicamentIdMedicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_Medicament_PrescriptionMedicamentIdPrescription_PrescriptionMedicamentIdMedicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropColumn(
                name: "PrescriptionMedicamentIdMedicament",
                table: "Prescription_Medicament");

            migrationBuilder.DropColumn(
                name: "PrescriptionMedicamentIdPrescription",
                table: "Prescription_Medicament");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionMedicamentIdMedicament",
                table: "Prescription_Medicament",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionMedicamentIdPrescription",
                table: "Prescription_Medicament",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "PrescriptionMedicamentIdMedicament", "PrescriptionMedicamentIdPrescription" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_Medicament_PrescriptionMedicamentIdPrescription_PrescriptionMedicamentIdMedicament",
                table: "Prescription_Medicament",
                columns: new[] { "PrescriptionMedicamentIdPrescription", "PrescriptionMedicamentIdMedicament" });

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Medicament_Prescription_Medicament_PrescriptionMedicamentIdPrescription_PrescriptionMedicamentIdMedicament",
                table: "Prescription_Medicament",
                columns: new[] { "PrescriptionMedicamentIdPrescription", "PrescriptionMedicamentIdMedicament" },
                principalTable: "Prescription_Medicament",
                principalColumns: new[] { "IdPrescription", "IdMedicament" });
        }
    }
}
