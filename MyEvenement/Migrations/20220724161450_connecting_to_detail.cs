using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEvenement.Migrations
{
    public partial class connecting_to_detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Detai",
                newName: "DetailID");

            migrationBuilder.AddColumn<int>(
                name: "DetailID",
                table: "Inscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeDetail",
                table: "Evenement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_DetailID",
                table: "Inscription",
                column: "DetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscription_Detai_DetailID",
                table: "Inscription",
                column: "DetailID",
                principalTable: "Detai",
                principalColumn: "DetailID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscription_Detai_DetailID",
                table: "Inscription");

            migrationBuilder.DropIndex(
                name: "IX_Inscription_DetailID",
                table: "Inscription");

            migrationBuilder.DropColumn(
                name: "DetailID",
                table: "Inscription");

            migrationBuilder.DropColumn(
                name: "TypeDetail",
                table: "Evenement");

            migrationBuilder.RenameColumn(
                name: "DetailID",
                table: "Detai",
                newName: "ID");
        }
    }
}
