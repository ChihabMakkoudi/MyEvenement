using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEvenement.Migrations
{
    public partial class Adding_statut2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscription_Statut_StatutID",
                table: "Inscription");

            migrationBuilder.AlterColumn<int>(
                name: "StatutID",
                table: "Inscription",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscription_Statut_StatutID",
                table: "Inscription",
                column: "StatutID",
                principalTable: "Statut",
                principalColumn: "StatutID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscription_Statut_StatutID",
                table: "Inscription");

            migrationBuilder.AlterColumn<int>(
                name: "StatutID",
                table: "Inscription",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscription_Statut_StatutID",
                table: "Inscription",
                column: "StatutID",
                principalTable: "Statut",
                principalColumn: "StatutID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
