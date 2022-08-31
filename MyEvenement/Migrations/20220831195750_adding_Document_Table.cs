using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEvenement.Migrations
{
    public partial class adding_Document_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileDocumentID",
                table: "Inscription",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileDocument",
                columns: table => new
                {
                    FileDocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taille = table.Column<long>(type: "bigint", nullable: false),
                    Fichier = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    References = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDocument", x => x.FileDocumentID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscription_FileDocumentID",
                table: "Inscription",
                column: "FileDocumentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscription_FileDocument_FileDocumentID",
                table: "Inscription",
                column: "FileDocumentID",
                principalTable: "FileDocument",
                principalColumn: "FileDocumentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscription_FileDocument_FileDocumentID",
                table: "Inscription");

            migrationBuilder.DropTable(
                name: "FileDocument");

            migrationBuilder.DropIndex(
                name: "IX_Inscription_FileDocumentID",
                table: "Inscription");

            migrationBuilder.DropColumn(
                name: "FileDocumentID",
                table: "Inscription");
        }
    }
}
