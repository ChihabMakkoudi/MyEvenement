using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEvenement.Migrations
{
    public partial class add_status_UserRef_Inscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Inscription",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Inscription",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Inscription");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Inscription");
        }
    }
}
