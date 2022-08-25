using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEvenement.Migrations
{
    public partial class Adding_statut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatutID",
                table: "Inscription",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Statut",
                columns: table => new
                {
                    StatutID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatutName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statut", x => x.StatutID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_StatutID",
                table: "Inscription",
                column: "StatutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscription_Statut_StatutID",
                table: "Inscription",
                column: "StatutID",
                principalTable: "Statut",
                principalColumn: "StatutID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscription_Statut_StatutID",
                table: "Inscription");

            migrationBuilder.DropTable(
                name: "Statut");

            migrationBuilder.DropIndex(
                name: "IX_Inscription_StatutID",
                table: "Inscription");

            migrationBuilder.DropColumn(
                name: "StatutID",
                table: "Inscription");
        }
    }
}
